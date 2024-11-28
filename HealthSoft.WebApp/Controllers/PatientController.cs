using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.RepositoryInterfaces;
using HealthSoft.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSoft.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PatientController(IPatientRepository patientRepository) : Controller
    {
        // GET: PatientController
        public async Task<ActionResult> Index()
        {
            var patients = await patientRepository.GetAllPatientsAsync();
            var mappedDoctors = patients.Select(dr => new PatientViewModel
            {
                Id = dr.Id,
                Name = $"{dr.FirstName} {dr.LastName}",
                Contact = dr.ContactNumber,
                Email = dr.Email,
                Gender = dr.Gender,
            });
            return View(mappedDoctors);
        }

        // GET: PatientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddPatientViewModel model)
        {
            try
            {
                var addPatientRequest = new AddPatientRequestDto
                {
                    Email = model.Email,
                    Password = model.Password,
                    Role = "Patient",
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    ContactNumber = model.ContactNumber,
                    Address = model.Address,

                };
                _ = await patientRepository.AddPatientAsync(addPatientRequest);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(model);
            }

        }

        // GET: PatientController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var patient = await patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return View();
            }
            var editPatientRequest = new AddPatientViewModel
            {
                Email = patient.Email,
                Role = "Patient",
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Gender = patient.Gender,
                ContactNumber = patient.ContactNumber,
                DateOfBirth = patient.DateOfBirth,
                Address = patient.Address,
            };
            return View(editPatientRequest);
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AddPatientViewModel model)
        {
            try
            {
                var editPatientModel = new EditPatientRequestDto
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    ContactNumber = model.ContactNumber,
                    DateOfBirth = model.DateOfBirth,
                    Address = model.Address,
                };
                _ = await patientRepository.UpdatePatientDetailsAsync(editPatientModel, id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        // GET: PatientController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _ = await patientRepository.DeletePatientAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
