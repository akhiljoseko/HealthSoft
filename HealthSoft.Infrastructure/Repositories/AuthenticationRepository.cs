using HealthSoft.Core.DTOs.ResponseDTOs;
using HealthSoft.Core.Entities;
using HealthSoft.Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthSoft.Infrastructure.Repositories
{
    public class AuthenticationRepository(
        UserManager<AppUser> userManager,
        IConfiguration configuration,
        SignInManager<AppUser> signInManager,
        IDoctorRepository doctorRepository,
        IPatientRepository patientRepository
        ) : IAuthenticationRepository
    {
        public async Task<bool> Logout()
        {
            await signInManager.SignOutAsync();
            return true;
        }

        public async Task<LoginResponseDto> PasswordLogin(LoginRequestDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null || !(await userManager.CheckPasswordAsync(user, request.Password)))
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid Email or Password",
                };
            var userRoles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email??"")
        };

            foreach (var role in userRoles)
            {
                if (role == "Doctor")
                {
                    int? doctorId = await doctorRepository.GetDoctorIdByUserIdAsync(user.Id);
                    if (doctorId != null)
                    {
                        claims.Add(new Claim("actorId", (doctorId ?? 0).ToString()));
                    }
                }
                if (role == "Patient")
                {
                    int? patientId = await patientRepository.GetPatientIdByUserIdAsync(user.Id);
                    if (patientId != null)
                    {
                        claims.Add(new Claim("actorId", (patientId ?? 0).ToString()));
                    }
                }
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            await userManager.AddClaimsAsync(user, claims);
            await signInManager.SignInAsync(user, new AuthenticationProperties { });

            return new LoginResponseDto
            {
                IsSuccess = true,
                Email = user.Email,
                AppUserId = user.Id,
            };
        }

        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var jwtKey = configuration["Jwt:Key"];
            var jwtIssuer = configuration["Jwt:Issuer"];

            if (jwtKey == null || jwtIssuer == null)
            {
                throw new ArgumentNullException("Invalid JWT crednetials");
            }

            return new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)), SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
