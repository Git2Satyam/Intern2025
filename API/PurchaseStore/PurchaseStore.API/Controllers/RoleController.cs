using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseStore.Models;
using PurchaseStore.Services.Interface;
using System.Diagnostics;

namespace PurchaseStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private ILogger<RoleController> _logger;    
        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
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
                    response.Status = "Ok";
                }
                else
                {
                    response.Success = false;
                    response.Status = "Failed";
                }
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteRole(string roleName)
        {
            try
            {
                throw new NotImplementedException();
                //var result = _roleService.DeleteRole(roleName);
                //if (result)
                //{
                //    return Ok(result);
                //}
                //return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
