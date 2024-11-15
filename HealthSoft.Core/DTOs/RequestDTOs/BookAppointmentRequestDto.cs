using System.ComponentModel.DataAnnotations;

namespace HealthSoft.Core.DTOs.RequestDTOs
{
    public class BookAppointmentRequestDto
    {
        [Required(ErrorMessage = "Patient ID is required.")]
        public required int PatientId { get; set; }

        // Doctor Details
        [Required(ErrorMessage = "Doctor ID is required.")]
        public required int DoctorId { get; set; }

        // Appointment Details
        [Required(ErrorMessage = "Appointment date and time is required.")]
        [DataType(DataType.DateTime)]
        public required DateTime AppointmentDateTime { get; set; }
        public required string Purpose { get; set; }

    }
}
