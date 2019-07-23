using DabeaV2.Common;
using DabeaV2.Entities;
using DabeaV2.Repositories.Interfaces;
using DabeaV2.Services.Interfaces;
using DabeaV2.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DabeaV2.Services
{
    public class TraegerService : ITraegerService
    {
        private readonly ILogger<TestService> _logger;
        private readonly AppSettings _options;
        private readonly IRepository _repository;

        public TraegerService(IOptions<AppSettings> options, ILogger<TestService> logger, IRepository repository)
        {
            _options = options.Value;
            _logger = logger;
            _repository = repository;
        }

        public async Task<TraegerNameResponseViewvModel> Get_Name(long id)
        {
            var traeger = await _repository.Get<Traeger>(x => x.Id == id);

            if(traeger == null)
            {
                throw new DabeaV2ServicesException($"Träger mit ID '{id}' nicht gefunden!");
            }

            return new TraegerNameResponseViewvModel
            {
                Name = traeger.Name.Trim() + (!string.IsNullOrEmpty(traeger.NameZusatz?.Trim()) ? ", " + traeger.NameZusatz?.Trim() : "")
            };
        }
    }

    public class TestService : ITestService
    {
        private readonly ILogger<TestService> _logger;
        private readonly AppSettings _options;
        private readonly IRepository _repository;

        public TestService(IOptions<AppSettings> options, ILogger<TestService> logger, IRepository repository)
        {
            _options = options.Value;
            _logger = logger;
            _repository = repository;
        }

        public async Task<Test1ResponseViewvModel> Test1()
        {
            try
            {


                //var transaction = _testDataService.BeginTransaction();
                Test1ResponseViewvModel result = null;
                using (var transaction = _repository.BeginTransaction())
                {
                    try
                    {
                        var benutzer = _repository.GetAll<Benutzer>().FirstOrDefault();

                        //benutzer.Person.Name += benutzer.Person.Name;

                        var kontakt = new Kontakt
                        {
                            Person = benutzer.Person,
                            IsActive = true,
                            Telefon = "1980",
                            Email = "ABC@ABC.ABC"
                        };

                        //await _repository.Add(kontakt);
                        await _repository.Add<Kontakt>(null);



                        transaction.Commit();

                        result = new Test1ResponseViewvModel
                        {
                            Name = benutzer.Person.Name
                        };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                return result;

            }
            catch (Exception ex)
            {
                throw new DabeaV2ServicesException("Test1 fehlgeschlagen!", ex);
            }
        }
        public async Task<Test1ResponseViewvModel> Test2()
        {
            using (var transaction = _repository.BeginTransaction())
            {
                try
                {
                    var benutzer = _repository.GetAll<Benutzer>().FirstOrDefault();

                    benutzer.Person.Name += benutzer.Person.Name;

                    await _repository.Update(benutzer);

                    transaction.Commit();

                    return new Test1ResponseViewvModel
                    {
                        Name = benutzer.Person.Name
                    };
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Test1 failed!");
                    transaction.Rollback();
                }
            }

            return null;
        }
    }
}
