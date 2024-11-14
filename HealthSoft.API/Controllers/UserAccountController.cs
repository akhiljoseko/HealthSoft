using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthSoft.API.Controllers
{
    [Route("api/v1/account")]
    [ApiController]
    public class UserAccountController(IUserAccountRepository accountRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUserAccount(CreateUserAccountDto request)
        {
            try
            {
                var createdUser = await accountRepository.CreateUserAccountAsync(request);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
