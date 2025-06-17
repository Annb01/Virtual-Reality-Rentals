using VR_Rentals.Models;

namespace VR_Rentals.Services
{
    public interface IVrEquipmentService
    {
        Task<List<VrEquipment>> GetAllAsync();
        Task<VrEquipment?> GetByIdAsync(int id);
        Task AddAsync(VrEquipment equipment);
        Task UpdateAsync(VrEquipment equipment);
        Task<bool> DeleteAsync(int equipmentId);

        Task<bool> ExistsAsync(int id);
        Task<List<VrEquipment>> GetAvailableAsync();
    }
}
