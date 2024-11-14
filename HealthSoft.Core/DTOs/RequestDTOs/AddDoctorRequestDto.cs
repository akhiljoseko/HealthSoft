using System.ComponentModel.DataAnnotations;

namespace HealthSoft.Core.DTOs.RequestDTOs
{
    public class AddDoctorRequestDto
    {
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

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        public required string Specialization { get; set; }

        [Required(ErrorMessage = "License number is required")]
        public required string LicenseNumber { get; set; }

        [Required(ErrorMessage = "Employement start date is required")]
        [DataType(DataType.Date)]
        public required DateTime EmploymentStartDate { get; set; }
        public DateTime? EmploymentEndDate { get; set; }

        [Required(ErrorMessage = "AppUserId is required")]
        public required string AppUserId { get; set; }
    }
}
