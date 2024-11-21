using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HealthSoft.WebApp.Models;
using HealthSoft.Core.RepositoryInterfaces;
using HealthSoft.Core.DTOs.RequestDTOs;

namespace HealthSoft.WebApp.Controllers;

public class HomeController(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository) : Controller
{
    

    public async Task<IActionResult> Index()
    {
        var appointments = await appointmentRepository.GetAllAppointments();
        var mappedModels = appointments.Select(ap => new AppointmentsModel
        {
            Id = ap.Id,
            DoctorName = $"{ap?.Doctor?.FirstName} {ap?.Doctor?.LastName}",
            PatientName = $"{ap?.Patient?.FirstName} {ap?.Patient?.LastName}"
        });
        return View(mappedModels);
    }

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
        });
        return View();
    }


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

    public async Task<IActionResult> Cancel(int? id)
    {
        if(id == null || id < 1)
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
