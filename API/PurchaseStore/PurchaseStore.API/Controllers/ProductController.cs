using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseStore.Models;
using PurchaseStore.Services.Interface;

namespace PurchaseStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var response = new ResponseModel();
            try
            {
                var result = _productService.GetAllProducts();
                if (result.Any())
                {
                    response.Success = true;
                    response.Status = "ok";
                    response.Result = result;
                }
                else
                {
                    response.Success = false;
                    response.Status = "Failed";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
