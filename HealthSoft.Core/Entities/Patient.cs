namespace HealthSoft.Core.Entities
{
    public class Patient : Person
    {
        public required string MedicalRecordNumber { get; set; }
        public string? Address { get; set; }


        public required string AppUserId { get; set; }
        public virtual required AppUser AppUser { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = [];
    }

}
