using FoodApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public IActionResult AdditemToCart(int productId)
        {
            var cookieExist = GetCookie();
            if(cookieExist == null)
            {
                // message
                return RedirectToAction("LoginForm", "Authentication", new { area = "Auth" });
            }
            else
            {
                var cartId = guid.ToString();
                var result = _cartService.AddItemToCart(productId, cartId);
                if (result)
                {
                    // Success message
                }
                else
                {
                    // Error message
                }
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        Guid guid
        {
            get
            {
                return Guid.NewGuid();
            }
        }
  

        private string GetCookie()
        {
            var cookie = HttpContext.Request.Cookies["FoodAppAuth"];
            if (cookie != null) return cookie.ToString();
            else return null;
        }
    }
}
