using VR_Rentals.Models;
using VR_Rentals.ViewModels;

namespace VR_Rentals.Services
{
    public interface IRentalService
    {
        Task<List<Rental>> GetAllAsync();
        Task<Rental?> GetByIdAsync(int id);
        Task AddAsync(Rental rental);
        Task UpdateAsync(Rental rental);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<Rental>> GetActiveRentalsForCustomerAsync(int customerId);
        Task<List<Rental>> GetRentalsByDateRangeAsync(DateTime from, DateTime to);
        Task<List<Rental>> GetOverdueRentalsAsync();
        Task<double> GetTotalRevenueAsync();
        Task<List<MyOrdersPageViewModel>> MyOrdersAsync(int customerId);

    }
}


