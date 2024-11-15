using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSoft.API.Controllers
{
    [Route("api/V1/account")]
    [Authorize(Policy = "AdminOnly")]
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

        [HttpDelete("{userAccountId}")]
        public async Task<IActionResult> DeleteUserAccount(string userAccountId)
        {
            try
            {
                bool isAccountDeleted = await accountRepository.DeleteUserAccountAsync(userAccountId);
                return Ok(new { isAccountDeleted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
