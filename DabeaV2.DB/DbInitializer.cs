using System;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using DabeaV2.Common;
using DabeaV2.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace DabeaV2.DB
{
    public static class DbInitializer
    {
        static ILogger logger;
        public static void Initialize(IServiceProvider services, DataContext context, AppSettings options)
        {
            logger = services.GetService<ILoggerFactory>().CreateLogger("DB.DbInitializer");

            try
            {
                context.Database.EnsureCreated();

                if (context.Personen.Any())
                {
                    return;
                }

                Seed(context, options);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private static void Seed(DataContext context, AppSettings options)
        {
            var traeger = new Traeger
            {
                IsActive = true,

                Name = "Sonnenschein",
                NameZusatz = "e.V."
            };

            traeger.Adressen.Add(new Adresse
            {
                IsActive = true,

                Strasse = "Am Teich",
                Ort = "Strausberg",
                Plz = "15344",
                Hausnummer = 7,
                HausnummerZusatz = "d"
            });
            context.Traeger.Add(traeger);

            // Rene Riesner
            var person_rr = new Person
            {
                IsActive = true,

                Name = "Riesner",
                VorName = "Rene",
            };

            person_rr.Adressen.Add(new Adresse
            {
                IsActive = true,

                Strasse = "Am Annatal",
                Ort = "Strausberg",
                Plz = "15344",
                Hausnummer = 11,
                HausnummerZusatz = "a"
            });

            person_rr.Benutzer.Add
            (
                new Benutzer
                {
                    IsActive = true,

                    UserName = "rr1980",
                    Passwort = GetHash("12003", options),
                }
            );

            person_rr.Kontakte.Add
            (
                new Kontakt
                {
                    IsActive = true,

                    Telefon = "12003",
                    Email = "r.riesner@computerzentrum.de",
                }
            );
            context.Personen.Add(person_rr);


            // Max Mustermann
            var person_mm = new Person
            {
                IsActive = true,

                Name = "Mustermann",
                VorName = "Max",
            };

            person_mm.Benutzer.Add
            (
                new Benutzer
                {
                    IsActive = true,
                    IsExtern = true,

                    UserName = "maxm",
                    Passwort = GetHash("12003", options),
                }
            );

            person_mm.Kontakte.Add
            (
                new Kontakt
                {
                    IsActive = true,

                    Telefon = "30021",
                    Email = "test123@321tset.test",
                }
            );

            context.Personen.Add(person_mm);


            context.SaveChanges();
        }

        public static string GetHash(string password, AppSettings options)
        {
            var salt = options.Security.PasswordSalt;

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
