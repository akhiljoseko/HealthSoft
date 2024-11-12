namespace HealthSoft.Core.Entities
{
    public class Appointment : BaseEntity
    {
        public DateTime AppointmentDateTime { get; set; }
        public required string ReasonForVisit { get; set; }

        // Foreign keys and navigation properties
        public int DoctorId { get; set; }
        public required Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public required Patient Patient { get; set; }
    }

}
