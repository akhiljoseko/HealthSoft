using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.RepositoryInterfaces;
using HealthSoft.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthSoft.WebApp.Controllers
{
    public class DoctorsController(IDoctorRepository doctorRepository) : Controller
    {
        // GET: DoctorsController
        public async Task<IActionResult> Index()
        {
            var doctors = await doctorRepository.GetAllDoctorsAsync();
            var mappedDoctors = doctors.Select(dr => new DoctorViewModel
            {
                Id = dr.Id,
                Name = $"{dr.FirstName} {dr.LastName}",
                Contact = dr.ContactNumber,
                Email = dr.Email,
                Gender = dr.Gender,
            });
            return View(mappedDoctors);
        }


        // GET: DoctorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddDoctorModel model)
        {
            try
            {
                var addDocorRequest = new AddDoctorRequestDto
                {
                    Email = model.Email,
                    Password = model.Password,
                    Role = "Doctor",
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    ContactNumber = model.ContactNumber,
                    Specialization = model.Specialization,
                    LicenseNumber = model.LicenseNumber,
                    EmploymentStartDate = model.EmploymentStartDate,
                    EmploymentEndDate = model.EmploymentEndDate,
                };
                _ = await doctorRepository.AddDoctorAsync(addDocorRequest);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var doctor = await doctorRepository.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return View();
            }
            var addDoctorModel = new AddDoctorModel
            {
                Email = doctor.Email,
                Role = "Doctor",
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Gender = doctor.Gender,
                ContactNumber = doctor.ContactNumber,
                Specialization = doctor.Specialization,
                LicenseNumber = doctor.LicenseNumber,
                EmploymentStartDate = doctor.EmploymentStartDate,
                EmploymentEndDate = doctor.EmploymentEndDate,
            };
            return View(addDoctorModel);
        }

        // POST: DoctorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AddDoctorModel model)
        {
            try
            {
                var editDocorRequest = new EditDoctorRequestDto
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    ContactNumber = model.ContactNumber,
                    Specialization = model.Specialization,
                    LicenseNumber = model.LicenseNumber,
                    EmploymentStartDate = model.EmploymentStartDate,
                    EmploymentEndDate = model.EmploymentEndDate,
                };

                _ = await doctorRepository.UpdateDoctorDetailsAsync( editDocorRequest, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _ = await doctorRepository.DeleteDoctorAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
