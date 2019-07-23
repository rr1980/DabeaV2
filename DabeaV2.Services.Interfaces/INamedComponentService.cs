using DabeaV2.Entities;
using DabeaV2.ViewModels;
using System;
using System.Threading.Tasks;

namespace DabeaV2.Services.Interfaces
{
    public interface IBaseComponentService
    {
    }

    public interface INamedComponentService : IBaseComponentService
    {
        Task<ComponentViewModel<NamePersonComponentViewModel>> Get_Person(RequestComponentViewModel request);
        Task<ComponentViewModel<NameBenutzerComponentViewModel>> Get_Benutzer(RequestComponentViewModel request);
    }
}
