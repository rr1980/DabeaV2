using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DabeaV2.Common;
using DabeaV2.Repositories.Interfaces;
using DabeaV2.Repositories;
using DabeaV2.DB;
using DabeaV2.Services.Interfaces;
using DabeaV2.Services;
using DabeaV2.Web.Middleware.ExceptionHandling;
using System.Runtime.Serialization;
using DabeaV2.Services.Components;

namespace DabeaV2.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), assembly => assembly.MigrationsAssembly("DabeaV2.DB"));
                //options.UseInMemoryDatabase(databaseName: "Rene_KitaTest");

                options.EnableDetailedErrors();
                //options.EnableSensitiveDataLogging();

            }, ServiceLifetime.Singleton);

            services.Configure<AppSettings>(o => Configuration.Bind(o));

            services.AddHttpContextAccessor();


            services.AddScoped<IRepository, Repository>();
            //services.AddScoped<IMainDataService, MainDataService>();
            //services.AddScoped<IBenutzerDataService, BenutzerDataService>();
            services.AddScoped<IBenutzerService, BenutzerService>();

            //services.AddScoped<ITestDataService, TestDataService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<INamedComponentService, NamedComponentService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                       .AddJwtBearer(jwtBearerOptions =>
                       {
                           jwtBearerOptions.SaveToken = true;
                           jwtBearerOptions.RequireHttpsMetadata = true;
                           jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                           {
                               ValidateIssuer = true,
                               ValidateActor = true,
                               ValidateAudience = true,
                               ValidateLifetime = true,
                               ValidateIssuerSigningKey = true,
                               ValidIssuer = Configuration["Security:Issuer"],
                               ValidAudience = Configuration["Security:Audience"],
                               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Security:SecurityKey"])),
                               SaveSigninToken = true,
                               ClockSkew = TimeSpan.Zero
                           };

                           jwtBearerOptions.Events = new JwtBearerEvents
                           {
                               OnAuthenticationFailed = context =>
                               {
                                   var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                                   logger.LogError("Authentication failed.", context.Exception);
                                   //if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                   //{
                                   //    context.Response.Headers.Add("Token-Expired", "true");
                                   //}

                                   return Task.FromException(context.Exception);
                               },
                           };
                       });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.ConfigureExceptionMiddleware();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
