using System.ComponentModel.DataAnnotations;

namespace HealthSoft.Core.DTOs.RequestDTOs
{
    public class CreateUserAccountDto
    {
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public required string Role { get; set; }
        
        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        
    }
}
