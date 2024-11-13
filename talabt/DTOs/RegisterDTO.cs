using System.ComponentModel.DataAnnotations;

namespace talabtAPIs.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[\\d\\W])[A-Za-z\\d\\W]{8,}$",
            ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter (A-Z), one lowercase letter (a-z), and one number or special character.\r\n")]
        public string Password {  get; set; }
    }
}
