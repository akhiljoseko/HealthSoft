using HealthSoft.Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthSoft.API.Controllers
{
    [Route("api/V1/auth")]
    [ApiController]
    public class AuthenticationController(IAuthenticationRepository authenticationRepository) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var loginResponse = await authenticationRepository.PasswordLogin(request);
                if (!loginResponse.IsSuccess)
                {
                    return Unauthorized(loginResponse.ErrorMessage);
                }
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
