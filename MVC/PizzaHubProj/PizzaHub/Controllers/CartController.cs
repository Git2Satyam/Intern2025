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
            var tokenExist = GetCookie();
            var cartExist = HttpContext.Request.Cookies["CartId"];
            if (tokenExist == null && cartExist == null)
            {
                TempData["Info"] = "Login First!";
                return RedirectToAction("Index", "Authentication", new { area = "Auth" });
            }
            else
            {
                
                if (cartExist != null)
                {
                    Guid cartId = new Guid(cartExist);
                    var result = _cartService.AddItemToCart(cartId, productId);
                    if (result == 0)
                    {
                        TempData["info"] = "Item already exist in cart!";
                    }
                    else if(result == 2)
                    {
                        TempData["success"] = "Item added to cart successfully!";
                    }
                }
                else
                {
                    Guid cartId = Cart;
                    var result = _cartService.AddItemToCart(cartId, productId);
                    if (result == 1)
                    {
                        SetCookie(cartId);
                        TempData["success"] = "Item added to cart successfully!";
                    }
                    else
                    {
                        TempData["error"] = "something went wrong!";
                    }
                }
                return RedirectToAction("GetProducts", "Product");
            }
           


            //return RedirectToAction("DisplayCartItems");
        }

        public IActionResult DisplayCartItems()
        {
            try
            {
                var cartItems = _cartService.GetCartItems();
                return View(cartItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Cart/UpdateQuantity")]
        public IActionResult UpdateQuantity(int productId, int qty)
        {
           
            try
            {
                var cartExist = HttpContext.Request.Cookies["CartId"];
                if (cartExist == null)
                {
                    TempData["error"] = "Cart does not exist";
                    return RedirectToAction("DisplayCartItems");
                }
                else
                {
                    var result = _cartService.UpdateQuantity(cartExist, productId, qty);    
                    if(result > 0)
                    {
                        //return RedirectToAction("DisplayCartItems");
                        return Json(result);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
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

        private void SetCookie(Guid cartId)
        {
            try
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMonths(1),
                    Path = "/",
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };
                Response.Cookies.Append("CartId", cartId.ToString(), cookieOptions);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public IActionResult CheckoutForCart(int? productId)
        {
            var cartExist = HttpContext.Request.Cookies["CartId"];
            if (cartExist != null)
            {
                var result = _cartService.CheckoutForCart(cartExist, productId);
                return View("Checkout", result);
            }
            else
            {
                return View();
            }

           
        }

        public IActionResult Checkout(int? productId)
        {
            return View("Checkout");
        }

        public IActionResult DeleteItem(int productId)
        {
            try
            {
                var result = _cartService.DeleteItem(productId);
                if (result)
                {
                    return RedirectToAction("DisplayCartItems");
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
