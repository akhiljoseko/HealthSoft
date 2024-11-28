using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.Entities;
using HealthSoft.Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HealthSoft.Infrastructure.Repositories
{
    public class AppointmentRepository(HealthSoftDbContext context, UserManager<AppUser> userManager) : IAppointmentRepository
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

        public async Task<IEnumerable<Appointment>> GetAllAppointments(string userId)
        {
            var user = await userManager.FindByIdAsync(userId) ?? throw new ArgumentException($"User not found with Id {userId}");
            var roles = await userManager.GetRolesAsync(user);
            if (roles.IsNullOrEmpty()) { return []; }

            return roles.First() switch
            {
                "Admin" => await GetAppointmentsForAdmin(),
                "Doctor" => await GetAppointmentForDoctor(user),
                "Patient" => await GetAppointmentForPatient(user),
                _ => [],
            };
        }

        private async Task<IEnumerable<Appointment>> GetAppointmentsForAdmin()
        {
            return await context.Appointments
                                   .Where(ap => ap.Status != "Cancelled")
                                   .Include(a => a.Doctor)
                                   .Include(a => a.Patient)
                                   .ToListAsync();
        }

        private async Task<IEnumerable<Appointment>> GetAppointmentForDoctor(AppUser user)
        {
            int doctorId = await context.Doctors
                .Where(doc => doc.AppUserId.Equals(user.Id))
                .Select(doc => doc.Id)
                .FirstOrDefaultAsync();
            return await context.Appointments
                   .Where(ap => !ap.Status.Equals("Cancelled") && ap.DoctorId.Equals(doctorId))
                   .Include(a => a.Doctor)
                   .Include(a => a.Patient)
                   .ToListAsync();
        }

        private async Task<IEnumerable<Appointment>> GetAppointmentForPatient(AppUser user)
        {
            int patientId = await context.Patients
                .Where(pat => pat.AppUserId.Equals(user.Id))
                .Select(pat => pat.Id)
                .FirstOrDefaultAsync();
            return await context.Appointments
                   .Where(ap => !ap.Status.Equals("Cancelled") && ap.PatientId.Equals(patientId))
                   .Include(a => a.Doctor)
                   .Include(a => a.Patient)
                   .ToListAsync();
        }

        public async Task<Appointment?> GetAppointmentById(int id)
        {
            return await context.Appointments
                .Where(a => a.Id == id)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync();
        }
       
        public async Task<bool> UpdateAppointment(BookAppointmentRequestDto requestDto, int id)
        {
            var appointment = await context.Appointments
               .FirstOrDefaultAsync(d => d.Id == id) ?? throw new ArgumentException("Appointment not found.");

            appointment.AppointmentDateTime = requestDto.AppointmentDateTime;
            appointment.DoctorId = requestDto.DoctorId;
            appointment.PatientId = requestDto.PatientId;
            appointment.ReasonForVisit = requestDto.Purpose;

            return await context.SaveChangesAsync() > 0;
        }
    }
}
