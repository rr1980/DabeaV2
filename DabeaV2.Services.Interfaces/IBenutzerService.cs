using DabeaV2.ViewModels;
using System.Threading.Tasks;

namespace DabeaV2.Services.Interfaces
{
    public interface IBenutzerService
    {
        long? GetCurrentBenutzerId();
        Task<BenutzerValidationResultModel> ValidateUser(string username, string password);

    }
}
