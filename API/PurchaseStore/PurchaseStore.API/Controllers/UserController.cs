using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PurchaseStore.Models;
using PurchaseStore.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PurchaseStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }


        [HttpPost]
        public IActionResult InsertOrUpdateUser(UserModel user)
        {
            var response = new ResponseModel();
            try
            {
                var result = _userService.InsertOrUpdateUser(user);
                if(result == 0 || result == 1)
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
        public IActionResult AuthenticateUser(string email, string password)
        {
            try
            {
                var response = new ResponseModel();
                var result = _userService.AuthenticateUser(email, password);
                if(!String.IsNullOrEmpty(result.Email))
                {
                    var claim = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, result.Email),
                        new Claim(ClaimTypes.NameIdentifier, result.FirstName),
                    };

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var secToken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                        claims: claim,
                        expires: DateTime.Now.AddMinutes(120),
                        signingCredentials: credentials,
                        audience: _configuration["Jwt:Audience"]
                    );
                    var token = new JwtSecurityTokenHandler().WriteToken(secToken);

                    response.Success = true;
                    response.Status = "Ok";
                    response.Result = token;
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
                return BadRequest($"{ex.Message}");
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUser()
        {
            try
            {
                var user = _userService.GetUsers();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAdminNavItems()
        {
            var response = new ResponseModel();
            try
            {
                var items = _userService.GetAdminNavItems();
                if(items == null)
                {
                    response.Success = false;
                    response.Status = "Failed";
                }
                else
                {
                    response.Success = true;
                    response.Status = "Ok";
                    response.Result = items;
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
