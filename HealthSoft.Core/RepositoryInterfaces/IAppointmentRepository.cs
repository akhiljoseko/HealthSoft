using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.Entities;

namespace HealthSoft.Core.RepositoryInterfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> BookAppointment(BookAppointmentRequestDto requestDto);
        Task<bool> CancelAppointment(int appointmentId);
        Task<IEnumerable<Appointment>> GetAllAppointments();
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorId(int doctorId);
    }
}
