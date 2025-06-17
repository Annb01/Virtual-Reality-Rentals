using Microsoft.AspNetCore.Identity;

namespace VR_Rentals.Models
{
    public class Customer : IdentityUser<int>
    {
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}



