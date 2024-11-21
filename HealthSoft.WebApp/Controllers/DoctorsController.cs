﻿using HealthSoft.Core.DTOs.RequestDTOs;
using HealthSoft.Core.Entities;
using HealthSoft.Core.RepositoryInterfaces;
using HealthSoft.WebApp.Models;
using Microsoft.AspNetCore.Http;
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

        // GET: DoctorsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoctorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
