using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.Entities;
using HealthSoft.Core.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthSoft.Infrastructure.Repositories
{
    public class PatientRepository(IUserAccountRepository userAccountRepository, HealthSoftDbContext context) : IPatientRepository
    {
        public async Task<Patient> AddPatientAsync(AddPatientRequestDto request)
        {
            AppUser appUser = await userAccountRepository.CreateUserAccountAsync(request);

            string patientMedicalRecordNumber = CreatePatientMedicalRecordNumber();

            Patient patient = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                ContactNumber = request.ContactNumber,
                Email = request.Email,
                MedicalRecordNumber = patientMedicalRecordNumber,
                Address = request.Address,
                AppUserId = appUser.Id,
                AppUser = appUser,
            };

            context.Patients.Add(patient);
            await context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> DeletePatientAsync(int patientId)
        {
            var patient = await context.Patients.FindAsync(patientId) ?? throw new ArgumentException("Patient not found.");
            context.Patients.Remove(patient);
            var isAppUserRemoved = await userAccountRepository.DeleteUserAccountAsync(patient.AppUserId);
            if (!isAppUserRemoved)
            {
                throw new ArgumentException("Failed to delete user account");
            }
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await context.Patients.ToListAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await context.Patients
               .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Patient> UpdatePatientDetailsAsync(AddPatientRequestDto requestDto, int PatientId)
        {
            var patient = await context.Patients
                .FirstOrDefaultAsync(d => d.Id == PatientId) ?? throw new ArgumentException("Patient not found.");

            patient.FirstName = requestDto.FirstName;
            patient.LastName = requestDto.LastName;
            patient.Gender = requestDto.Gender;
            patient.DateOfBirth = requestDto.DateOfBirth;
            patient.ContactNumber = requestDto.ContactNumber;
            patient.Email = requestDto.Email;
            patient.Address = requestDto.Address;
            
            await context.SaveChangesAsync();

            return patient;
        }

        #region Private Methods
        private string CreatePatientMedicalRecordNumber()
        {
            // Get the current date formatted as YYYYMM
            string dateSegment = DateTime.Now.ToString("yyyyMM");

            // Retrieve the last MRN created in the current month
            var lastMrn = context.Patients
                                  .Where(p => p.MedicalRecordNumber.StartsWith($"PAT-{dateSegment}-"))
                                  .OrderByDescending(p => p.MedicalRecordNumber)
                                  .FirstOrDefault()?.MedicalRecordNumber;

            // Extract the incremental part and increment it, or start at 1 if no records found
            int nextIncrement = 1;
            if (lastMrn != null)
            {
                string lastIncrementString = lastMrn.Split('-').Last();
                if (int.TryParse(lastIncrementString, out int lastIncrement))
                {
                    nextIncrement = lastIncrement + 1;
                }
            }

            // Format the MRN as PAT-YYYYMM-XXXX
            string newMrn = $"PAT-{dateSegment}-{nextIncrement:D4}";

            return newMrn;


        }

        #endregion
    }
}
