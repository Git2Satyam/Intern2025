using FoodApp.Core.DB_Context;
using FoodApp.Core.Entities;
using FoodApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace FoodApp.UI.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class AuthenticationController : Controller
    {
        private FoodAppContext _context;
        public AuthenticationController(FoodAppContext context)
        {
            _context = context;
        }
        public IActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult VerifyUser(LoginFormModel model)
        {
            var user = new UserModel();
            var userExist = _context.Users.FirstOrDefault(us => us.Email == model.Username);
            if (userExist != null)
            {
                user.Id = userExist.Id;
                user.FirstName = userExist.FirstName;
                user.LastName = userExist.LastName;
                user.Email = userExist.Email;

                GenerateTicket(user);
                return RedirectToAction("Index", "Home", new {area=""});
            }
            else
            {
                return RedirectToAction("LoginForm");
            }
           
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(UserModel user)
        {
            try
            {
                var addUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    Password = user.Password,
                    PasswordExpiryDate = DateTime.Now.AddMonths(6),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };
                _context.Users.Add(addUser);
                _context.SaveChanges(); 
                return RedirectToAction("LoginForm");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void GenerateTicket(UserModel user)
        {
            var userData = JsonSerializer.Serialize(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserData, userData),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.FirstName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            new AuthenticationProperties()
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.AddMinutes(10),
                IsPersistent = true
            };
        }

    }
}
