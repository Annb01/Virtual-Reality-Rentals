using VR_Rentals.Models;
using VR_Rentals.Repositories;

namespace VR_Rentals.Services
{
    public class VrEquipmentService : IVrEquipmentService
    {
        private readonly IVrEquipmentRepository _repository;

        public VrEquipmentService(IVrEquipmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<VrEquipment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<VrEquipment?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(VrEquipment equipment)
        {
            await _repository.AddAsync(equipment);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(VrEquipment equipment)
        {
            await _repository.UpdateAsync(equipment);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int equipmentId)
        {
            var equipment = await _repository.GetByIdAsync(equipmentId);
            if (equipment == null)
                return false;

            var hasActiveRentals = equipment.Rentals.Any(r => r.ReturnDate == null);
            if (hasActiveRentals)
                return false; 

            await _repository.DeleteAsync(equipment.EquipmentId);
            return true;
        }


        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }

        public async Task<List<VrEquipment>> GetAvailableAsync()
        {
            return await _repository.GetAvailableAsync();
        }
    }
}
