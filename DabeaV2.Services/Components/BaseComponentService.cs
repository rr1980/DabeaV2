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

    public abstract class BaseComponentService : IBaseComponentService
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
        protected async Task<ComponentViewModel<TComponentViewModel>> BuildResult<TComponentViewModel, TEntity>(RequestComponentViewModel request, Func<TEntity, TComponentViewModel, TComponentViewModel> componentViewModelMapperFunc) where TEntity : BaseEntity where TComponentViewModel : BaseComponentViewModel
        {
            var entity = await _repository.Get<TEntity>(x => x.Id == request.Id);

            if (entity == null)
            {
                throw new Exception($"Entity von Type '{request.EntityType.ToString()}' und Id '{request.Id}' konnte nicht gefunden werden!");
            }

            TComponentViewModel vm = (TComponentViewModel)Activator.CreateInstance<TComponentViewModel>();

            return new ComponentViewModel<TComponentViewModel>
            {
                Id = entity.Id,
                EntityType = request.EntityType,
                Result = componentViewModelMapperFunc(entity, vm)
            };
        }

        //protected async Task<ComponentViewModel<TComponentViewModel>> BuildResult<TComponentViewModel, TEntity>(RequestComponentViewModel request, Func<TEntity, TComponentViewModel> componentViewModelMapperFunc) where TEntity : BaseEntity where TComponentViewModel : BaseComponentViewModel
        //{
        //    var entity = await _repository.Get<TEntity>(x => x.Id == request.Id);

        //    if(entity == null)
        //    {
        //        throw new Exception($"Entity von Type '{request.EntityType.ToString()}' und Id '{request.Id}' konnte nicht gefunden werden!");
        //    }

        //    return new ComponentViewModel<TComponentViewModel>
        //    {
        //        Id = entity.Id,
        //        EntityType = request.EntityType,
        //        Result = componentViewModelMapperFunc(entity)
        //    };
        //}

        protected TComponentViewModel SetBasics<TEntity, TComponentViewModel>(TEntity entity, TComponentViewModel componentViewModel) where TEntity : BaseEntity where TComponentViewModel : BaseComponentViewModel
        {
            componentViewModel.Id = entity.Id;
            componentViewModel.IsActive = entity.IsActive;

            return componentViewModel;
        }
    }
}
