using DabeaV2.Common;
using DabeaV2.Entities;
using DabeaV2.Repositories.Interfaces;
using DabeaV2.Services.Interfaces;
using DabeaV2.ViewModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DabeaV2.Services
{
    public class BenutzerService : IBenutzerService
    {
        private readonly AppSettings _options;
        private readonly IRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BenutzerService(IOptions<AppSettings> options, IRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _options = options.Value;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public long? GetCurrentBenutzerId()
        {
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null)
            {
                var _uId = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").Select(x => x.Value).FirstOrDefault();

                if (!string.IsNullOrEmpty(_uId))
                {
                    var uId = long.Parse(_uId);

                    if (uId == 0)
                    {
                        throw new Exception("UserID not Found!");
                    }

                    return uId;
                }
            }

            return null;
        }

        public async Task<BenutzerValidationResultModel> ValidateUser(string username, string password)
        {
            var result = new BenutzerValidationResultModel();

            var benutzer = await _repository.Get<Benutzer>(x => x.UserName.ToLower().Trim() == username.ToLower().Trim());

            if (benutzer == null)
            {
                result.Fail = true;
                result.ErrMsg = "Benutzername oder Passwort falsch!";
            }
            else
            {
                var loginPasswordHash = GetHash(password);
                if (benutzer.Passwort != loginPasswordHash)
                {
                    result.Fail = true;
                    result.ErrMsg = "Benutzername oder Passwort falsch!";
                }
                else
                {
                    if (!benutzer.IsActive)
                    {
                        result.Fail = true;
                        result.ErrMsg = "Benutzer deaktiviert, bitte wenden Sie sich an den Administrator!";
                    }
                }

                result.Benutzer = new UserViewvModel
                {
                    Id = benutzer.Id,
                    IsExtern = benutzer.IsExtern,
                    Username = benutzer.UserName,
                    FirstName = benutzer.Person.VorName,
                    LastName = benutzer.Person.Name
                };
            }

            benutzer.LastLogin = DateTime.Now;
            await _repository.Update(benutzer, false);

            return result;
        }

        private string GetHash(string password)
        {
            var salt = _options.Security.PasswordSalt;

            if (string.IsNullOrEmpty(salt))
            {
                throw new NullReferenceException("Konnte 'PasswordSalt' in Configuration nicht finden!");
            }

            if (salt.Length < 20)
            {
                for (int i = salt.Length; salt.Length < 20; i++)
                {
                    salt += i;
                }
            }
            return ComputeHash(salt, password);
        }

        private static string ComputeHash(string salt, string password)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256));
        }
    }
}
