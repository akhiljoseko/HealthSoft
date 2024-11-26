using HealthSoft.Core.RepositoryInterfaces;
using HealthSoft.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthSoft.WebApp.Controllers
{
    public class LoginController(IAuthenticationRepository authenticationRepository) : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index (LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {

                var loginRequest = new LoginRequestDto
                {
                    Email = loginModel.EmailId,
                    Password = loginModel.Password,
                };
                var result = await authenticationRepository.PasswordLogin(loginRequest);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid login credentials. Please try again";
                    return View(loginModel);
                }
            }
            return View(loginModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            bool didSignedOut = await authenticationRepository.Logout();
            if (didSignedOut)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
