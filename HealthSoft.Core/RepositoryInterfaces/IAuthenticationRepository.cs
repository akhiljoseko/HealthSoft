using HealthSoft.Core.DTOs.ResponseDTOs;

namespace HealthSoft.Core.RepositoryInterfaces
{
    public interface IAuthenticationRepository
    {
        Task<bool> Logout();
        Task<LoginResponseDto> PasswordLogin(LoginRequestDto loginRequestDto);
    }
}
