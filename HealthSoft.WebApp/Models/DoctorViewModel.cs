namespace HealthSoft.WebApp.Models
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Gender { get; set; }
        public string? Contact { get; set; }

        public string? Email { get; set; }
    }
}
