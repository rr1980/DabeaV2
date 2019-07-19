using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DabeaV2.Common;
using DabeaV2.Services.Interfaces;
using DabeaV2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DabeaV2.Web.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly AppSettings _options;
        private readonly IBenutzerService _benutzerService;

        public AccountController(IOptions<AppSettings> options, IBenutzerService benutzerService)
        {
            _options = options.Value;
            _benutzerService = benutzerService;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginViewvModel loginViewvModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _benutzerService.ValidateUser(loginViewvModel.Username, loginViewvModel.Password);

                if (result.Fail)
                {
                    var br = BadRequest(new { Msg = result.ErrMsg });
                    br.StatusCode = (int?)HttpStatusCode.Unauthorized;
                    return br;
                }

                if (result.Benutzer == null)
                {
                    throw new NullReferenceException();
                }

                var claims = new List<Claim>()
                {
                    new Claim("UserId", result.Benutzer.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                //foreach (var role in loginUserResultViewData.Zugriffsbeschraenkungen)
                //{
                //    claims.Add(new Claim("role", role.ToString()));
                //}

                var now = DateTime.UtcNow;

                var token = new JwtSecurityToken
                (
                    issuer: _options.Security.Issuer,
                    audience: _options.Security.Audience,
                    claims: claims.ToArray(),
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(_options.Security.LoginExpires)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Security.SecurityKey)), SecurityAlgorithms.HmacSha256)
                );


                var rt = new JwtSecurityTokenHandler().WriteToken(token);

                result.Benutzer.Token = rt;

                return Ok(result.Benutzer);
            }

            return BadRequest(new UnauthorizedAccessException("Eingabe konnte nicht verarbeitet werden!"));
        }

        //[Authorize]
        //[Throttle(Name = "UserInfo", Seconds = 5)]
        //[HttpPost("UserInfo")]
        //public async Task<IActionResult> UserInfo()
        //{
        //    long userId = long.Parse(User.FindFirst("UserId").Value, CultureInfo.CurrentCulture);
        //    UserInfoResultViewModel userInfo = await _loginDataService.GetUserInfo(userId).ConfigureAwait(false);

        //    var appVersion = typeof(Program).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

        //    var claims = new List<Claim>()
        //        {
        //            new Claim("Version", appVersion),
        //            new Claim("IsNew", userInfo.IsNew.ToString(CultureInfo.CurrentCulture)),
        //            new Claim("IsExtern", userInfo.IsExtern.ToString(CultureInfo.CurrentCulture)),
        //            new Claim("UserId", userInfo.Id.ToString(CultureInfo.CurrentCulture)),
        //            new Claim("Name", userInfo.Name),
        //            new Claim("Vorname", userInfo.VorName),
        //            new Claim("Email", (!string.IsNullOrEmpty(userInfo.EMail) ? userInfo.EMail : "") ),
        //            new Claim(JwtRegisteredClaimNames.Sub, userInfo.LoginName),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        };

        //    if (_options.Test)
        //    {
        //        claims.Add(new Claim("IsTest", "True"));
        //    }

        //    foreach (var role in userInfo.Zugriffsbeschraenkungen)
        //    {
        //        claims.Add(new Claim("role", role.ToString()));
        //    }

        //    var now = DateTime.UtcNow;

        //    var token = new JwtSecurityToken
        //    (
        //        issuer: _options.Issuer,
        //        audience: _options.Audience,
        //        claims: claims.ToArray(),
        //        notBefore: now,
        //        expires: now.Add(TimeSpan.FromMinutes(_options.LoginExpires)),
        //        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey)),
        //             SecurityAlgorithms.HmacSha256)
        //    );


        //    var rt = new JwtSecurityTokenHandler().WriteToken(token);
        //    return Ok(new { token = rt });
        //}

        //[Throttle(Name = "PasswordRecovery", Seconds = 5)]
        //[HttpPost("PasswordRecovery")]
        //public async Task<IActionResult> PasswordRecovery([FromBody]PasswordRecoveryViewData vm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _loginDataService.PasswordRecovery(vm).ConfigureAwait(false);
        //        return Ok();
        //    }

        //    return BadRequest(new UnauthorizedAccessException("Eingabe konnte nicht verarbeitet werden!"));
        //}
    }
}
