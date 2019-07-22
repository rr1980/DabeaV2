using DabeaV2.Common;
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

    public abstract class BaseComponentService<TComponentViewModel> : IBaseComponentService<TComponentViewModel>
    {
        protected readonly ILogger _logger;
        protected readonly AppSettings _options;
        protected readonly IRepository _repository;

        public BaseComponentService(IOptions<AppSettings> options, ILogger logger, IRepository repository)
        {
            _options = options.Value;
            _logger = logger;
            _repository = repository;
        }

        public abstract Task<ComponentViewModel<TComponentViewModel>> Get(ComponentViewModel<TComponentViewModel> request);

        protected async Task<ComponentViewModel<TComponentViewModel>> BuildResult<T>(ComponentViewModel<NameComponentViewModel> request, Func<T, TComponentViewModel> componentViewModelMapperFunc) where T :BaseEntity
        {
            var entity = await _repository.GetActive<T>(x => x.Id == request.Id);

            if(entity == null)
            {
                throw new Exception($"Entity von Type '{request.EntityType.ToString()}' und Id '{request.Id}' konnte nicht gefunden werden!");
            }

            return new ComponentViewModel<TComponentViewModel>
            {
                Id = entity.Id,
                EntityType = request.EntityType,
                Result = componentViewModelMapperFunc(entity)
            };
        }
    }
}
