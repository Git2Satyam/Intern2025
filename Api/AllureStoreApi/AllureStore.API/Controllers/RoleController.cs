using AllureStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllureStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public IActionResult InsertOrUpdateRole(AdminRoleModel model)
        {
            try
            {

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
