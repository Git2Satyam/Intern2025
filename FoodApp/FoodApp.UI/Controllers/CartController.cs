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

        [Route("Cart/UpdateQuantity")]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            try
            {
                var cartExistId = HttpContext.Request.Cookies["CartId"];
                if(cartExistId == null)
                {
                    TempData["error"] = "Login First!";
                    return RedirectToAction("LoginForm", "Authentication", new { area = "Auth" });
                }
                else
                {
                    var result = _cartService.UpdateQuantity(productId, quantity);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Delete(int productId)
        {
            try
            {
                var result = _cartService.DeleteItem(productId);
                if (result)
                {
                    // messasge
                    return RedirectToAction("ShowCartItems");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Checkout(int productId)
        {
            try
            {
                var cartExistId = HttpContext.Request.Cookies["CartId"];
                if (cartExistId == null)
                {
                    TempData["error"] = "Login First!";
                    return RedirectToAction("LoginForm", "Authentication", new { area = "Auth" });
                }
                else
                {
                    var result = _cartService.Checkout(productId, cartExistId);
                    return View("Checkout",result);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult CheckoutForHome(int productId)
        {
            var authToken = Request.Cookies["FoodAppAuth"];
            if(authToken == null)
            {
                TempData["error"] = "Login First!";
                return RedirectToAction("LoginForm", "Authentication", new { area = "Auth" });
            }
            else
            {
                var result = _cartService.CheckoutCheckoutForHome(productId);
                return View("Checkout", result);

            }
        }
    }
}
