using VR_Rentals.Models;

namespace VR_Rentals.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<Customer?> FindByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);

    }
}