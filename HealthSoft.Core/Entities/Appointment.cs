namespace HealthSoft.Core.Entities
{
    public class Appointment : BaseEntity
    {
        public DateTime AppointmentDateTime { get; set; }
        public required string ReasonForVisit { get; set; }
        public required string Status { get; set; }

        // Foreign keys
        public required int DoctorId { get; set; }
        public required int PatientId { get; set; }

        // Navigation properties
        public virtual  Doctor? Doctor { get; set; }
        public virtual  Patient? Patient { get; set; }
    }

}
