using FoodApp.Services.Interface;
using FoodApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.WebSockets;

namespace FoodApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private IWebHostEnvironment _webHostEnvironment;
        public HomeController(IProductService productService, ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
       

        public IActionResult Index()
        {
            try
            {
                ViewBag.SliderImages = GetSliderImages();
                var products = _productService.GetProductDetails();
                if (products.Any())
                {
                    return View(products);
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
        
        private List<string> GetSliderImages()
        {
            var imageUrl = new List<string>();
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Slider");
            string[] imagePaths = Directory.GetFiles(imagePath);
            foreach (string path in imagePaths)
            {
                string relativePath = path.Replace(_webHostEnvironment.WebRootPath, "").Replace("\\", "/");
                imageUrl.Add(relativePath);
            }

            return imageUrl;
        }
    }
}
