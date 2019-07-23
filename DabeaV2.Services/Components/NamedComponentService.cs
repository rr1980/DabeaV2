using DabeaV2.Common;
using DabeaV2.Common.Enums;
using DabeaV2.Entities;
using DabeaV2.Repositories.Interfaces;
using DabeaV2.Services.Interfaces;
using DabeaV2.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DabeaV2.Services.Components
{
    public class NamedComponentService : BaseComponentService, INamedComponentService
    {
        private readonly IBenutzerService _benutzerService;

        public NamedComponentService(IOptions<AppSettings> options, ILogger<NamedComponentService> logger, IRepository repository, IBenutzerService benutzerService) : base(options, logger, repository)
        {
            _benutzerService = benutzerService;
        }

        public Task<ComponentViewModel<NamePersonComponentViewModel>> Get_Person(RequestComponentViewModel request)
        {
            return BuildResult<NamePersonComponentViewModel, Person>(request, (entity, vm) => 
            {
                vm.Name = entity.Name;
                vm.VorName = entity.VorName;
                vm.FullName = entity.Name + ", " + entity.VorName;

                return vm;
            });
        }

        public Task<ComponentViewModel<NameBenutzerComponentViewModel>> Get_Benutzer(RequestComponentViewModel request)
        {
            request.Id = _benutzerService.GetCurrentBenutzerId();

            return BuildResult<NameBenutzerComponentViewModel, Benutzer>(request, (entity, vm) =>
            {
                vm.Name = entity.Person.Name;
                vm.VorName = entity.Person.VorName;
                vm.FullName = entity.Person.Name + ", " + entity.Person.VorName;
                vm.UserName = entity.UserName;
                return vm;
            });
        }


        //public Task<ComponentViewModel<NamePersonComponentViewModel>> Get_Person(RequestComponentViewModel request)
        //{
        //    return BuildResult<NamePersonComponentViewModel, Person>(request, entity => SetBasics(entity, new NamePersonComponentViewModel
        //    {
        //        Name = entity.Name,
        //        VorName = entity.VorName,
        //        FullName = entity.Name + ", " + entity.VorName
        //    }));
        //}
    }
}
