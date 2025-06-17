using VR_Rentals.Models;
using VR_Rentals.ViewModels;

namespace VR_Rentals.Services
{
    public interface IAccountService
    {
        Task<Customer?> AuthenticateAsync(string email, string password);
        Task RegisterAsync(RegisterPageViewModel model);
    }

}
