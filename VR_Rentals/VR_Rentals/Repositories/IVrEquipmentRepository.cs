using VR_Rentals.Models;

namespace VR_Rentals.Repositories
{
    public interface IVrEquipmentRepository
    {
        Task<List<VrEquipment>> GetAllAsync();
        Task<VrEquipment?> GetByIdAsync(int id);
        Task AddAsync(VrEquipment equipment);
        Task UpdateAsync(VrEquipment equipment);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<VrEquipment>> GetAvailableAsync();
        Task SaveChangesAsync();
    }
}
