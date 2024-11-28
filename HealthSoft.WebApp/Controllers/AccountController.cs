using Microsoft.AspNetCore.Mvc;

namespace HealthSoft.WebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
