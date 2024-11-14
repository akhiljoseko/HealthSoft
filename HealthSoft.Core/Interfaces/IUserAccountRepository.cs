﻿using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.Entities;

namespace HealthSoft.Core.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<AppUser> CreateUserAccountAsync(CreateUserAccountDto request);
        Task<bool> DeleteUserAccountAsync(string userId);
    }
}
