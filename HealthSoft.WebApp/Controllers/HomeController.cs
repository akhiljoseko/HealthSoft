using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HealthSoft.WebApp.Models;
using HealthSoft.Core.RepositoryInterfaces;
using HealthSoft.Core.DTOs.RequestDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HealthSoft.Core.Entities;

namespace HealthSoft.WebApp.Controllers;

[Authorize]
public class HomeController(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository, UserManager<AppUser> userManager) : Controller
{


    public async Task<IActionResult> Index()
    {
        string? userId = userManager.GetUserId(User);
            if(userId == null)
        {
            return Unauthorized();
        }

        
        var appointments = await appointmentRepository.GetAllAppointments(userId);
        var mappedModels = appointments.Select(ap => new AppointmentsModel
        {
            Id = ap.Id,
            DoctorName = $"{ap?.Doctor?.FirstName} {ap?.Doctor?.LastName}",
            PatientName = $"{ap?.Patient?.FirstName} {ap?.Patient?.LastName}"
        });

        return View(mappedModels);
    }

    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Add()
    {
        var doctors = await doctorRepository.GetAllDoctorsAsync();
        ViewBag.Doctors = doctors.Select(dr => new DoctorViewModel
        {
            Id = dr.Id,
            Name = $"{dr.FirstName} {dr.LastName}",
            Contact = dr.ContactNumber,
            Email = dr.Email,
            Gender = dr.Gender,
        });

        var patients = await patientRepository.GetAllPatientsAsync();
        ViewBag.Patients = patients.Select(pt => new PatientViewModel
        {
            Id = pt.Id,
            Name = $"{pt.FirstName} {pt.LastName}",
            Contact = pt.ContactNumber,
            Email = pt.Email,
            Gender = pt.Gender,
        });
        return View();
    }

    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Create(CreateAppointmentViewModel createAppointmentModel)
    {
        if (ModelState.IsValid)
        {
            var createAppointmentRequest = new BookAppointmentRequestDto
            {
                DoctorId = createAppointmentModel.DoctorId,
                PatientId = createAppointmentModel.PatientId,
                AppointmentDateTime = createAppointmentModel.AppointmentDate,
                Purpose = createAppointmentModel?.Reason ?? "N/A"
            };
            _ = await appointmentRepository.BookAppointment(createAppointmentRequest);
            return RedirectToAction("Index", "Home");
        }
        return View(createAppointmentModel);
    }

    [Authorize(Roles ="Admin")]
    public async Task<ActionResult> Edit(int id)
    {
        var doctors = await doctorRepository.GetAllDoctorsAsync();
        ViewBag.Doctors = doctors.Select(dr => new DoctorViewModel
        {
            Id = dr.Id,
            Name = $"{dr.FirstName} {dr.LastName}",
            Contact = dr.ContactNumber,
            Email = dr.Email,
            Gender = dr.Gender,
        });

        var patients = await patientRepository.GetAllPatientsAsync();
        ViewBag.Patients = patients.Select(pt => new PatientViewModel
        {
            Id = pt.Id,
            Name = $"{pt.FirstName} {pt.LastName}",
            Contact = pt.ContactNumber,
            Email = pt.Email,
            Gender = pt.Gender,
        });
        var appointment = await appointmentRepository.GetAppointmentById(id);
        if (appointment == null)
        {
            return View();
        }
        var editAppointmentModel = new CreateAppointmentViewModel
        {
            AppointmentDate = appointment.AppointmentDateTime,
            DoctorId = appointment.DoctorId,
            PatientId = appointment.PatientId,
            Reason = appointment.ReasonForVisit,
        };
        return View(editAppointmentModel);
    }

    [Authorize(Roles ="Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, CreateAppointmentViewModel model)
    {
        try
        {
            var updateAppointmentRequest = new BookAppointmentRequestDto
            {
                AppointmentDateTime = model.AppointmentDate,
                DoctorId = model.DoctorId,
                PatientId = model.PatientId,
                Purpose = model.Reason ?? "N/A",
            };

            _ = await appointmentRepository.UpdateAppointment(updateAppointmentRequest, id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Cancel(int? id)
    {
        if (id == null || id < 1)
        {
            return RedirectToAction("Index", "Home");
        }
        _ = await appointmentRepository.CancelAppointment((int)id);
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Privacy()
    {
        return View();
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
