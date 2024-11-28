using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.Entities;

namespace HealthSoft.Core.RepositoryInterfaces
{
    public interface IPatientRepository
    {
        Task<Patient?> GetPatientByIdAsync(int id);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> AddPatientAsync(AddPatientRequestDto request);
        Task<Patient> UpdatePatientDetailsAsync(EditPatientRequestDto requestDto, int PatientId);
        Task<bool> DeletePatientAsync(int PatientId);
        Task<int?> GetPatientIdByUserIdAsync(string id);
    }
}
