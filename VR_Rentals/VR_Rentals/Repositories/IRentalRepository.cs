using System.Linq.Expressions;
using VR_Rentals.Models;

namespace VR_Rentals.Repositories
{
    public interface IRentalRepository
    {
        Task<List<Rental>> GetAllAsync();
        Task<Rental?> GetByIdAsync(int id);
        Task AddAsync(Rental rental);
        Task UpdateAsync(Rental rental);
        Task DeleteAsync(Rental rental);
        Task<bool> ExistsAsync(int id);
        Task<List<Rental>> GetWhereAsync(Expression<Func<Rental, bool>> predicate);
        Task<List<Rental>> RentalsWithEquipment(int customerId);

    }
}
