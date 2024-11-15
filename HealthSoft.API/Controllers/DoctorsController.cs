using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthSoft.API.Controllers
{
    [Route("api/V1/doctors")]
    [ApiController]
    public class DoctorsController(IDoctorRepository doctorRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {
                var doctors = await doctorRepository.GetAllDoctorsAsync();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            try
            {
                var doctor = await doctorRepository.GetDoctorByIdAsync(id);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctorAccount([FromBody] AddDoctorRequestDto requestDto)
        {
            try
            {
                var createdDoctor = await doctorRepository.AddDoctorAsync(requestDto);
                return Ok(createdDoctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {ex.Message});
            }
        }     
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctorDetails(int id, [FromBody] AddDoctorRequestDto requestDto)
        {
            try
            {
                var updatedDetails = await doctorRepository.UpdateDoctorDetailsAsync(requestDto, id);
                return Ok(updatedDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                bool isDeleted = await doctorRepository.DeleteDoctorAsync(id);
                return Ok(new {isDocotorDeleted = isDeleted});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
