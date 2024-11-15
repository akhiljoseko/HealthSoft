using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthSoft.Api.Controllers
{
    [Route("api/V1/appointments")]
    [ApiController]
    public class AppointmentsController(IAppointmentRepository appointmentRepository) : ControllerBase
    {

        [HttpPost("Book")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentRequestDto requestDto)
        {
            
            try
            {
                var appointment = await appointmentRepository.BookAppointment(requestDto);
                return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Cancel/{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            try
            {
                var result = await appointmentRepository.CancelAppointment(id);
                if (!result)
                    return NotFound($"Appointment with ID {id} not found.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            try
            {
                var appointments = await appointmentRepository.GetAllAppointments();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpGet("Doctor/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsByDoctorId(int doctorId)
        {
            try
            {
                var appointments = await appointmentRepository.GetAppointmentsByDoctorId(doctorId);
                if (!appointments.Any())
                    return NotFound($"No appointments found for Doctor ID {doctorId}.");

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            try
            {
                var appointments = await appointmentRepository.GetAllAppointments();
                var appointment = appointments.FirstOrDefault(a => a.Id == id);

                if (appointment == null)
                    return NotFound($"Appointment with ID {id} not found.");

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
