namespace HealthSoft.WebApp.Models
{
    public class CreateAppointmentViewModel
    {
        public DateTime AppointmentDate { get; set; }  
        public int DoctorId { get; set; }              
        public int PatientId { get; set; }             
        public string? Reason { get; set; }
    }
}
