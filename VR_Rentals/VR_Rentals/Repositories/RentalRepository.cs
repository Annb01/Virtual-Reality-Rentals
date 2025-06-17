using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VR_Rentals.Models;

namespace VR_Rentals.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly RentalContext _context;

        public RentalRepository(RentalContext context)
        {
            _context = context;
        }

        public async Task<List<Rental>> GetAllAsync()
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.VrEquipment)
                .ToListAsync();
        }

        public async Task<Rental?> GetByIdAsync(int id)
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.VrEquipment)
                .FirstOrDefaultAsync(r => r.RentalId == id);
        }

        public async Task AddAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Rental rental)
        {
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Rentals.AnyAsync(r => r.RentalId == id);
        }
        public async Task<List<Rental>> GetWhereAsync(Expression<Func<Rental, bool>> predicate)
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.VrEquipment)
                .Where(predicate)
                .ToListAsync();
        }
        public async Task<List<Rental>> RentalsWithEquipment(int customerId)
        {
            return await _context.Rentals
                .Include(r => r.VrEquipment)
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

    }
}
