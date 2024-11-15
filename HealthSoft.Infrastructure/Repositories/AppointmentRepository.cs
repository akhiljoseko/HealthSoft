using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.Entities;
using HealthSoft.Core.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthSoft.Infrastructure.Repositories
{
    public class AppointmentRepository(HealthSoftDbContext context) : IAppointmentRepository
    {
        public async Task<Appointment> BookAppointment(BookAppointmentRequestDto requestDto)
        {
            var appointment = new Appointment
            {
                DoctorId = requestDto.DoctorId,
                PatientId = requestDto.PatientId,
                ReasonForVisit = requestDto.Purpose,
                AppointmentDateTime = requestDto.AppointmentDateTime,
                Status = "Booked" 
            };

            await context.Appointments.AddAsync(appointment);
            await context.SaveChangesAsync();

            return appointment;
        }

        public async Task<bool> CancelAppointment(int appointmentId)
        {
            var appointment = await context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
                return false;

            appointment.Status = "Cancelled";
            context.Appointments.Update(appointment);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            return await context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorId(int doctorId)
        {
            return await context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();
        }
    }
}
