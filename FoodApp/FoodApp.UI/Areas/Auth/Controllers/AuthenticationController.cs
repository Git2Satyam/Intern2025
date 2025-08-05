using Microsoft.AspNetCore.Mvc;

namespace FoodApp.UI.Areas.Auth.Controllers
{
    public class AuthenticationController : Controller
    {
        [Area("Auth")]
        public IActionResult LoginForm()
        {
            return View();
        }

        public IActionResult SignupForm()
        {
            return View(); 
        }
    }
}
