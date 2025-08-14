using Microsoft.AspNetCore.Mvc;
using PizzaHub.Services.Interface;

namespace PizzaHub.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public IActionResult AddItemToCart(int productId)
        {
            var cookieExist = GetCookie();
            if (cookieExist == null)
            {
                TempData["Info"] = "Login First!";
                return RedirectToAction("Index", "Authentication", new { area = "Auth" });
            }
            Guid cartId = Cart;
            var result = _cartService.AddItemToCart(cartId, productId);
            if (result)
            {
                TempData["success"] = "Item added to cart successfully!";
            }
            else
            {
                TempData["error"] = "something went wrong!";
            }
            return RedirectToAction("GetProducts", "Product");
            //return RedirectToAction("DisplayCartItems");
        }

      

        public IActionResult DisplayCartItems()
        {
            try
            {
                var cartItems = _cartService.GetCartItems();
                if(cartItems.Any())
                {
                    return View(cartItems);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult UpdateQuantity(int productId, int existQty, int qty)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        Guid Cart
        {
            get
            {
                return Guid.NewGuid();
            }
        }

        private string GetCookie()
        {
            var cookie = HttpContext.Request.Cookies["PizzaHubAuth"];
            if (cookie != null) return cookie.ToString();
            else return null;
        }
    }

    
    
}
