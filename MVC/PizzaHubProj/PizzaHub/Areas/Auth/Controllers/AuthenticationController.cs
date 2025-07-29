using Microsoft.AspNetCore.Mvc;

namespace PizzaHub.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignupForm()
        {
            return View();
        }
    }
}
