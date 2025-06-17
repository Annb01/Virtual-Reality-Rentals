using Microsoft.EntityFrameworkCore;
using VR_Rentals.Data;
using VR_Rentals.Models;

namespace VR_Rentals.Repositories
{
    public class VrEquipmentRepository : IVrEquipmentRepository
    {
        private readonly RentalContext _context;

        public VrEquipmentRepository(RentalContext context)
        {
            _context = context;
        }

        public async Task<List<VrEquipment>> GetAllAsync()
        {
            return await _context.VrEquipments.ToListAsync();
        }

        public async Task<VrEquipment?> GetByIdAsync(int id)
        {
            return await _context.VrEquipments.FindAsync(id);
        }

        public async Task AddAsync(VrEquipment equipment)
        {
            await _context.VrEquipments.AddAsync(equipment);
        }

        public async Task UpdateAsync(VrEquipment equipment)
        {
            _context.VrEquipments.Update(equipment);
        }

        public async Task DeleteAsync(int id)
        {
            var equipment = await GetByIdAsync(id);
            if (equipment != null)
            {
                _context.VrEquipments.Remove(equipment);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.VrEquipments.AnyAsync(e => e.EquipmentId == id);
        }

        public async Task<List<VrEquipment>> GetAvailableAsync()
        {
            return await _context.VrEquipments
                .Where(e => e.EquipmentStatus == Status.Available)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
