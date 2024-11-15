using System.ComponentModel.DataAnnotations;

namespace HealthSoft.Core.DTOs.RequestDTOs
{
    public class AddDoctorRequestDto : CreateUserAccountDto
    {
        [Required(ErrorMessage = "Specialization is required")]
        public required string Specialization { get; set; }

        [Required(ErrorMessage = "License number is required")]
        public required string LicenseNumber { get; set; }

        [Required(ErrorMessage = "Employement start date is required")]
        [DataType(DataType.Date)]
        public required DateTime EmploymentStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EmploymentEndDate { get; set; }
    }
}
