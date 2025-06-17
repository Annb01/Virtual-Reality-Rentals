using AutoMapper;
using VR_Rentals.Models;
using VR_Rentals.Repositories;
using VR_Rentals.ViewModels;

namespace VR_Rentals.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.CustomerName) || customer.CustomerName.Length < 2)
                throw new ArgumentException("Customer first name is required and must be at least 2 characters long.");

            if (string.IsNullOrWhiteSpace(customer.CustomerSurname) || customer.CustomerSurname.Length < 2)
                throw new ArgumentException("Customer last name is required and must be at least 2 characters long.");

            if (string.IsNullOrWhiteSpace(customer.Email) || !customer.Email.Contains("@"))
                throw new ArgumentException("Invalid email address.");

            var existsByEmail = await _customerRepository.FindByEmailAsync(customer.Email);
            if (existsByEmail != null)
                throw new InvalidOperationException("A customer with this email address already exists.");

            if (string.IsNullOrWhiteSpace(customer.PasswordHash))
                throw new ArgumentException("Password is required.");

            var existsById = await _customerRepository.ExistsAsync(customer.CustomerId);
            if (existsById)
                throw new InvalidOperationException("A customer with this ID already exists.");

            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _customerRepository.ExistsAsync(id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _customerRepository.ExistsByEmailAsync(email);
        }

        public async Task UpdateAccountAsync(int id, AccountPageViewModel model)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            _mapper.Map(model, customer);

            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                if (string.IsNullOrEmpty(model.CurrentPassword) ||
                    !BCrypt.Net.BCrypt.Verify(model.CurrentPassword, customer.PasswordHash))
                    throw new UnauthorizedAccessException("Current password is incorrect");

                customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            }

            await _customerRepository.UpdateAsync(customer);
        }
    }
}
