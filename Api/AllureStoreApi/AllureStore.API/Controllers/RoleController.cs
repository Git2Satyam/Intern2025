using AllureStore.Models;
using AllureStore.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllureStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetAllRole()
        {
            var response = new ResponseModel();
            try
            {
                var result = _roleService.GetAllRoles();
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

        [HttpPost]
        public IActionResult InsertOrUpdateRole(AdminRoleModel model)
        {
            var response = new ResponseModel();
            try
            {
                var result = _roleService.InsertOrUpdateRole(model);
                if(result == 1)
                {
                    response.Success = true;
                    response.Status = "ok";
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
