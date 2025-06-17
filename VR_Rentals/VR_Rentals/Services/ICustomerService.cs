using VR_Rentals.Models;
using VR_Rentals.ViewModels;

namespace VR_Rentals.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email);
        Task UpdateAccountAsync(int id, AccountPageViewModel model);
    }
}


