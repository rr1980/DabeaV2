using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DabeaV2.Web.Middleware.ExceptionHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {JsonConvert.SerializeObject(ex, Formatting.Indented)}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = GetExceptionMessage(exception),
                Stack = GetExceptionStack(exception)
            }.ToString());
        }

        private static string GetExceptionMessage(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            var counter = 0;

            Exception currentEx = ex;
            while (currentEx.InnerException != null)
            {
                counter++;

                sb.Append($"{counter}. {currentEx.Message.Trim()}");

                if (currentEx.InnerException != null)
                {
                    sb.Append(Environment.NewLine);
                    Tabbed(sb, counter);
                }

                currentEx = currentEx.InnerException;
            }

            return sb.ToString();
        }

        private static string GetExceptionStack(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            var counter = 0;

            Exception currentEx = ex;
            while (currentEx.InnerException != null)
            {

                SplitStack(sb, counter, currentEx);

                counter++;

                Tabbed(sb, counter);

                if (currentEx.InnerException != null)
                {
                    sb.Append(Environment.NewLine);
                }

                currentEx = currentEx.InnerException;
            }

            return sb.ToString();
        }

        private static void SplitStack(StringBuilder sb, int counter, Exception ex)
        {
            var stackArray = ex.StackTrace.Split(Environment.NewLine);

            if (stackArray.Any())
            {
                Tabbed(sb, counter);

                sb.Append($"{counter + 1}. " + ex.Message.Trim() + Environment.NewLine);

                Tabbed(sb, counter + 1);

                sb.Append(stackArray[0].Trim() + Environment.NewLine);

                Tabbed(sb, counter + 1);

                for (int i = 1; i < stackArray.Count(); i++)
                {
                    var item = stackArray[i];

                    sb.Append($"{item.Trim()}" + Environment.NewLine);

                    if (i + 1 < stackArray.Count())
                    {
                        Tabbed(sb, counter + 1);
                    }
                }
            }
        }
        private static void Tabbed(StringBuilder sb, int counter)
        {
            for (int i = 0; i < counter; i++)
            {
                sb.Append("\t");
            }
        }
    }
}