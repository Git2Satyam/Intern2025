using Microsoft.AspNetCore.Mvc;
using PizzaHub.Services.Interface;

namespace PizzaHub.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _hostingEnvironment = webHostEnvironment;
        }
        public IActionResult GetProducts()
        {
            try
            {
                ViewBag.Images = GetSliderImages();
                var products = _productService.GetProducts();
                return View(products);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        private List<string> GetSliderImages()
        {
            List<string> imageUrls = new List<string>();
            try
            {
                var imagesPath = Path.Combine(_hostingEnvironment.WebRootPath, "Images/Slider/");
                IEnumerable<string> imagePaths = Directory.EnumerateFiles(imagesPath, "*.jpg");
                foreach (string filePath in imagePaths)
                {
                    string fileName = Path.GetFileName(filePath);
                    imageUrls.Add($"/Images/Slider/{fileName}");
                }
                return imageUrls;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
