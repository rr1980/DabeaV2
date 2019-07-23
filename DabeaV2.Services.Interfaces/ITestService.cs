using DabeaV2.ViewModels;
using System.Threading.Tasks;

namespace DabeaV2.Services.Interfaces
{
    public interface ITraegerService
    {
        Task<TraegerNameResponseViewvModel> Get_Name(long id);
    }

    public interface ITestService
    {
        Task<Test1ResponseViewvModel> Test1();
    }
}
