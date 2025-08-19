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
            var tokenExist = GetCookie();
            var cartExistId = HttpContext.Request.Cookies["CartId"];
            if(tokenExist == null && cartExistId == null)
            {
                TempData["error"] = "Login First!";
                return RedirectToAction("LoginForm", "Authentication", new { area = "Auth" });
            }
            else
            {
                if(cartExistId != null)
                {
                    var result = _cartService.AddItemToCart(productId, cartExistId);
                    if(result == 2)
                    {
                        TempData["sucess"] = "Item added to cart successfully!";
                    }
                    else if(result == 0)
                    {
                        TempData["sucess"] = "Item already exist in cart!";
                    }
                }
                else
                {
                    var cartId = guid.ToString();
                    var result = _cartService.AddItemToCart(productId, cartId);
                    if (result == 1)
                    {
                        Response.Cookies.Append("CartId", cartId, new CookieOptions { Expires = DateTime.Now.AddMonths(1), SameSite = SameSiteMode.Lax, HttpOnly = true, IsEssential = true });
                        TempData["success"] = "Item added to cart successfully";
                    }
                    else
                    {
                        TempData["error"] = "Something went wrong!";
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult ShowCartItems()
        {
            var cartExist = Request.Cookies["CartId"];
            if(cartExist == null)
            {
                TempData["error"] = "Login First!";
                return RedirectToAction("LoginForm", "Authentication", new { area = "Auth" });
            }
            else
            {
                Guid cartId = new Guid(cartExist);
                var cartItems = _cartService.GetProducts(cartId);
                return View(cartItems);
            }
            
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
