using Microsoft.EntityFrameworkCore;
using VR_Rentals.Models;

namespace VR_Rentals.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RentalContext _context;

        public CustomerRepository(RentalContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync(){
            return await _context.Customers.ToListAsync();
        } 

        public async Task<Customer?> GetByIdAsync(int id){
            return await _context.Customers.FindAsync(id);
        }

        public async Task AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id){
            return await _context.Customers.AnyAsync(e => e.CustomerId == id);
        }

        public async Task<Customer?> FindByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }
        public async Task<Customer?> RentalsByCustomerIdAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Rentals)
                    .ThenInclude(r => r.VrEquipment)
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Customers.AnyAsync(c => c.Email == email);
        }

    }
}

