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
        [RegularExpression("(?= ^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\n])(?=.*[A - Z])(?=.*[a - z]).*$",
            ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter (A-Z), one lowercase letter (a-z), and one number or special character.\r\n")]
        public string Password {  get; set; }
    }
}
