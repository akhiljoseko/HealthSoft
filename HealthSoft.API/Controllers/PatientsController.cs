using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSoft.API.Controllers
{
    [Route("api/V1/patients")]
    [ApiController]
    public class PatientsController(IPatientRepository patientRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllpatients()
        {
            try
            {
                var patients = await patientRepository.GetAllPatientsAsync();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getpatient(int id)
        {
            try
            {
                var patient = await patientRepository.GetPatientByIdAsync(id);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatepatientAccount([FromBody] AddPatientRequestDto requestDto)
        {
            try
            {
                var createdpatient = await patientRepository.AddPatientAsync(requestDto);
                return Ok(createdpatient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatepatientDetails(int id, [FromBody] EditPatientRequestDto requestDto)
        {
            try
            {
                var updatedDetails = await patientRepository.UpdatePatientDetailsAsync(requestDto, id);
                return Ok(updatedDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepatient(int id)
        {
            try
            {
                bool isDeleted = await patientRepository.DeletePatientAsync(id);
                return Ok(new { isDocotorDeleted = isDeleted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
