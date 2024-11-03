using System.ComponentModel.DataAnnotations;

namespace Talabt.APIS.DTO
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
        [RegularExpression("(?=^.{6,10}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&amp;*()_+]).*$",
            ErrorMessage ="Password must contain 1Uppercase,1Lowercase,1 Digit,1 Spaecial Character")]
        public string Password { get; set; }

    }
}
