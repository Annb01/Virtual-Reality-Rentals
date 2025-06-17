using System.ComponentModel.DataAnnotations;

namespace VR_Rentals.ViewModels
{
    public class AccountPageViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "New password must be at least 6 characters")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmNewPassword { get; set; }
    }
}
