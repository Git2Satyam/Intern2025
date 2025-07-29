using Microsoft.AspNetCore.Mvc;
using PizzaHub.Services.Interface;

namespace PizzaHub.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productService.GetProducts();
                return View(products);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
