using DabeaV2.ViewModels;
using System.Threading.Tasks;

namespace DabeaV2.Services.Interfaces
{
    public interface IBaseComponentService<TComponentViewModel>
    {
        Task<ComponentViewModel<TComponentViewModel>> Get(ComponentViewModel<TComponentViewModel> request);
    }

    public interface INamedComponentService : IBaseComponentService<NameComponentViewModel>
    {
    }
}
