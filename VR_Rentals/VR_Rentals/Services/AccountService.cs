using AutoMapper;
using VR_Rentals.Models;
using VR_Rentals.Repositories;
using VR_Rentals.ViewModels;

namespace VR_Rentals.Services
{
    public class AccountService : IAccountService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public AccountService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Customer?> AuthenticateAsync(string email, string password)
        {
            var customer = await _customerRepository.FindByEmailAsync(email);
            if (customer != null && BCrypt.Net.BCrypt.Verify(password, customer.PasswordHash))
            {
                return customer;
            }

            return null;
        }

        public async Task RegisterAsync(RegisterPageViewModel model)
        {
            if (await _customerRepository.ExistsByEmailAsync(model.Email))
                throw new ArgumentException("Email is already taken.");

            var customer = _mapper.Map<Customer>(model);
            customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            await _customerRepository.AddAsync(customer);
        }
    }

}
