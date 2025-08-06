using FoodApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.UI.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class AuthenticationController : Controller
    {
        public IActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerifyUser(LoginFormModel model)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

    }
}
