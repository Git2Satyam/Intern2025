using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseStore.Models;
using PurchaseStore.Services.Interface;

namespace PurchaseStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly I_ImageService _imageService;
        public ImageController(I_ImageService imageService) => _imageService = imageService;


        [HttpPost]
        public IActionResult SaveImage([FromForm] IFormFile imageFile, [FromQuery] int productId)
        {
            var response = new ResponseModel();
            try
            {
                var result = _imageService.SaveImage(imageFile, productId);
                if (result)
                {
                    response.Success = true;
                    response.Status = "Ok";
                }
                else
                {
                    response.Success= false;
                    response.Status = "Failed";
                }
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
