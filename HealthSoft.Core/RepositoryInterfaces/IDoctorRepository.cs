using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.Entities;

namespace HealthSoft.Core.RepositoryInterfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor?> GetDoctorByIdAsync(int id);
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> AddDoctorAsync(AddDoctorRequestDto request);
        Task<Doctor> UpdateDoctorDetailsAsync(AddDoctorRequestDto requestDto, int doctorId);
        Task<bool> DeleteDoctorAsync(int doctorId);
    }
}
