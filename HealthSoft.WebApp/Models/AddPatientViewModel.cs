using System.ComponentModel.DataAnnotations;

namespace HealthSoft.WebApp.Models
{
    public class AddPatientViewModel
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public required string Role { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [MaxLength(1, ErrorMessage = "Gender should be a character M/F/O")]
        [RegularExpression(@"^[MFO]$", ErrorMessage = "Gender must be one of the following: M, F, or O")]
        public required string Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid contact number")]
        public required string ContactNumber { get; set; }
        public string? Address { get; set; }
    }
}
