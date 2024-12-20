﻿using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.Entities;
using HealthSoft.Core.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthSoft.Infrastructure.Repositories
{
    public class DoctorRepository(HealthSoftDbContext context, IUserAccountRepository userAccountRepository) : IDoctorRepository
    {
        private readonly HealthSoftDbContext _context = context;
        private readonly IUserAccountRepository _userAccountRepository = userAccountRepository;

        public async Task<Doctor> AddDoctorAsync(AddDoctorRequestDto request)
        {
            
            AppUser appUser = await _userAccountRepository.CreateUserAccountAsync(request);

            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                ContactNumber = request.ContactNumber,
                Email = request.Email,
                Specialization = request.Specialization,
                LicenseNumber = request.LicenseNumber,
                EmploymentStartDate = request.EmploymentStartDate,
                EmploymentEndDate = request.EmploymentEndDate,
                AppUserId = appUser.Id, 
                AppUser = appUser,
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<bool> DeleteDoctorAsync(int doctorId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId) ?? throw new ArgumentException("Doctor not found.");

            var isAppUserRemoved = await _userAccountRepository.DeleteUserAccountAsync(doctor.AppUserId);
            if (!isAppUserRemoved)
            {
                throw new ArgumentException("Failed to delete user account");
            }

            doctor.IsDeleted = true;
            doctor.DeletedDateTime = DateTime.UtcNow;
            _context.Update(doctor);

            
            return await _context.SaveChangesAsync() > 0;  
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<int?> GetDoctorIdByUserIdAsync(string id)
        {
            return await _context.Doctors
                .Where(doc => doc.AppUserId == id)
                .Select(doc => doc.Id)
                .FirstOrDefaultAsync();
                
        }

        public async Task<Doctor> UpdateDoctorDetailsAsync(EditDoctorRequestDto requestDto, int doctorId)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.Id == doctorId) ?? throw new ArgumentException("Doctor not found.");

            doctor.FirstName = requestDto.FirstName;
            doctor.LastName = requestDto.LastName;
            doctor.Gender = requestDto.Gender;
            doctor.DateOfBirth = requestDto.DateOfBirth;
            doctor.ContactNumber = requestDto.ContactNumber;
            doctor.Specialization = requestDto.Specialization;
            doctor.LicenseNumber = requestDto.LicenseNumber;
            doctor.EmploymentStartDate = requestDto.EmploymentStartDate;
            doctor.EmploymentEndDate = requestDto.EmploymentEndDate;

            await _context.SaveChangesAsync();

            return doctor;
        }
    }
}
