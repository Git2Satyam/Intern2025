using AllureStore.Models;
using AllureStore.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AllureStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost]
        public IActionResult InsertOrUpdateUser(UserModel model)
        {
            var response = new ResponseModel();
            try
            {
                var result = _userService.InsertOrUpdateUser(model);
                if(result == 0 ||  result == 1)
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

        [HttpGet]
        public IActionResult VerifyUser(string email, string password)
        {
            try
            {
                var response = new ResponseModel();
                var result = _userService.VerifyUser(email, password);
                if (result.Id > 0)
                {
                    var claim = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, result.Email),
                        new Claim(ClaimTypes.NameIdentifier, result.FirstName)
                    };

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
                    var credential = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

                    var secToken = new JwtSecurityToken(_config["Jwt:Issuer"],
                        claims: claim,
                        expires: DateTime.Now.AddMinutes(120),
                        signingCredentials: credential,
                        audience: _config["Jwt:Audience"]
                        );

                    var token = new JwtSecurityTokenHandler().WriteToken(secToken);

                    response.Success = true;
                    response.Status = "ok";
                    response.Result = token;
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

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUser()
        {
            try
            {
                var response = new ResponseModel();
                var result = _userService.GetAllUser();
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
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
