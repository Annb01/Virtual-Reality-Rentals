using AutoMapper;
using VR_Rentals.Models;
using VR_Rentals.ViewModels;
using VR_Rentals.Repositories;

namespace VR_Rentals.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IVrEquipmentRepository _equipmentRepository;
        private readonly IMapper _mapper;

        public RentalService(
            IRentalRepository rentalRepository,
            IVrEquipmentRepository equipmentRepository,
            IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _equipmentRepository = equipmentRepository;
            _mapper = mapper;
        }

        public async Task<List<Rental>> GetAllAsync()
        {
            return await _rentalRepository.GetAllAsync();
        }

        public async Task<Rental?> GetByIdAsync(int id)
        {
            return await _rentalRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Rental rental)
        {
            await _rentalRepository.AddAsync(rental);

            var equipment = await _equipmentRepository.GetByIdAsync(rental.EquipmentId);
            if (equipment != null)
            {
                equipment.EquipmentStatus = Status.Rented;
                await _equipmentRepository.UpdateAsync(equipment);
            }
        }

        public async Task UpdateAsync(Rental rental)
        {
            await _rentalRepository.UpdateAsync(rental);
        }

        public async Task DeleteAsync(int id)
        {
            var rental = await _rentalRepository.GetByIdAsync(id);
            if (rental != null)
            {
                await _rentalRepository.DeleteAsync(rental);

                var equipment = await _equipmentRepository.GetByIdAsync(rental.EquipmentId);
                if (equipment != null)
                {
                    equipment.EquipmentStatus = Status.Available;
                    await _equipmentRepository.UpdateAsync(equipment);
                }
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _rentalRepository.ExistsAsync(id);
        }

        public async Task<List<Rental>> GetActiveRentalsForCustomerAsync(int customerId)
        {
            return await _rentalRepository.GetWhereAsync(r =>
                r.CustomerId == customerId && r.ReturnDate == null);
        }

        public async Task<List<Rental>> GetRentalsByDateRangeAsync(DateTime from, DateTime to)
        {
            return await _rentalRepository.GetWhereAsync(r =>
                r.RentalDate >= from && r.RentalDate <= to);
        }

        public async Task<List<Rental>> GetOverdueRentalsAsync()
        {
            return await _rentalRepository.GetWhereAsync(r =>
                r.PlannedReturnDate < DateTime.Now && r.ReturnDate == null);
        }

        public async Task<double> GetTotalRevenueAsync()
        {
            var rentals = await _rentalRepository.GetAllAsync();
            return rentals.Sum(r => r.TotalRentalCost);
        }

        public async Task<List<MyOrdersPageViewModel>> MyOrdersAsync(int customerId)
        {
            var rentals = await _rentalRepository.RentalsWithEquipment(customerId);
            return rentals.Select(r => _mapper.Map<MyOrdersPageViewModel>(r)).ToList();
        }
    }
}
