using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.Entities;
using HealthSoft.Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace HealthSoft.Infrastructure.Repositories
{
    public class UserAccountRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) : IUserAccountRepository
    {
        public async Task<AppUser> CreateUserAccountAsync(CreateUserAccountDto request)
        {
            var user = new AppUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,

            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            if (!await roleManager.RoleExistsAsync(request.Role))
            {
                await roleManager.CreateAsync(new IdentityRole(request.Role));
            }

            var roleResult = await userManager.AddToRoleAsync(user, request.Role);

            if (!roleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleResult.Errors.Select(e => e.Description)));
            }

            //switch (request.Role.ToLower())
            //{
            //    case "doctor":
            //        await _doctorService.AddDoctorAsync(new Doctor { UserId = createdUser.Id, /* other doctor details */ });
            //        break;
            //    case "patient":
            //        await _patientService.AddPatientAsync(new Patient { UserId = createdUser.Id, /* other patient details */ });
            //        break;
            //    case "admin":
            //        // If Admin has special handling, do it here or just add role to user
            //        break;
            //    default:
            //        throw new ArgumentException("Invalid role specified.");
            //}

            return user;
        }

        public async Task<bool> DeleteUserAccountAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var result = await userManager.DeleteAsync(user);

            return result.Succeeded;
        }
    }

}
