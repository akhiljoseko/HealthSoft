namespace HealthSoft.Core.Entities
{
    public class Doctor : Person
    {
        public required string Specialization { get; set; }
        public required string LicenseNumber { get; set; }
        public required DateTime EmploymentStartDate { get; set; }
        public DateTime? EmploymentEndDate { get; set; }
        public bool IsActive { get; set; } = false;
        public required string AppUserId { get; set; }
        public virtual required AppUser AppUser { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; } = [];
    }

}
