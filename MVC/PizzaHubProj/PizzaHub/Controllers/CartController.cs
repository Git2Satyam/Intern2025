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
            //return RedirectToAction("GetProducts", "Product");
            return RedirectToAction("DisplayCartItems");
        }

        Guid Cart
        {
            get
            {
               return Guid.NewGuid();
            }
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
    }

    
    
}
