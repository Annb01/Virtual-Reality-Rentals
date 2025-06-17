using System.ComponentModel.DataAnnotations;

namespace VR_Rentals.ViewModels
{
    public class RegisterPageViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string CustomerSurname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
