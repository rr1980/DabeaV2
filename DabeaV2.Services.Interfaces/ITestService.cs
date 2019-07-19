using DabeaV2.ViewModels;
using System.Threading.Tasks;

namespace DabeaV2.Services.Interfaces
{
    public interface ITestService
    {
        Task<Test1ResponseViewvModel> Test1();
    }
}
