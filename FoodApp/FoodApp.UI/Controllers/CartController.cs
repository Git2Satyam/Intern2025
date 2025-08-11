using Microsoft.AspNetCore.Mvc;

namespace FoodApp.UI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult AdditemToCart()
        {
            var cookieExist = GetCookie();
            if(cookieExist == null)
            {
                // message
                return RedirectToAction("LoginForm", "Authentication", new { area = "Auth" });
            }
            return View();
        }

        private string GetCookie()
        {
            var cookie = HttpContext.Request.Cookies[".AspNetCore.Antiforgery.HYvTDXPBmAM"];
            if (cookie != null) return cookie.ToString();
            else return null;
        }
    }
}
