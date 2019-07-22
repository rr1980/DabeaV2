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
    public class NamedComponentService : BaseComponentService<NameComponentViewModel>, INamedComponentService
    {
        public NamedComponentService(IOptions<AppSettings> options, ILogger<NamedComponentService> logger, IRepository repository) : base(options, logger, repository)
        {
        }

        public override Task<ComponentViewModel<NameComponentViewModel>> Get(ComponentViewModel<NameComponentViewModel> request)
        {

            Task<ComponentViewModel<NameComponentViewModel>> result = null;

            switch (request.EntityType)
            {
                case EntityType.Person:
                    result = Build_Perso_Get(request);
                    break;
                default:
                    throw new Exception($"EntiyType '{request.EntityType.ToString()}' unbekannt!");
            }

            return result;
        }

        private Task<ComponentViewModel<NameComponentViewModel>> Build_Perso_Get(ComponentViewModel<NameComponentViewModel> request)
        {
            return BuildResult<Person>(request, entity => new NameComponentViewModel
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                Name = entity.Name,
                VorName = entity.VorName,
                FullName = entity.Name + ", " + entity.VorName
            });
        }
    }
}
