using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzaHub.Core.DB_Context;
using PizzaHub.Core.Entities;
using PizzaHub.Models;
using System.Security.Claims;

namespace PizzaHub.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class AuthenticationController : Controller
    {
        private readonly PizzaHubContext _context;
        public AuthenticationController(PizzaHubContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignupForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerifyUser(LoginFormModel login)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == login.UserName && x.Password == login.Password);
                if (user == null)
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    var userML = new UserModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Password = user.Password,
                    };
                    GenerateTicket(userML);
                    return RedirectToAction("GetProducts", "Product", new { Area = "" });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult InsertOrUpdateUser(UserModel model)
        {
            bool success = false;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    var userExist = _context.Users.FirstOrDefault(x => x.Id == model.Id);
                    if (userExist != null)
                    {
                        userExist.Email = model.Email;
                        userExist.Password = model.Password;
                        userExist.PhoneNumber = model.PhoneNumber;
                        userExist.FirstName = model.FirstName;
                        userExist.LastName = model.LastName;
                        userExist.Deleted = false;
                        userExist.ModifiedDate = DateTime.Now;
                        userExist.ModifiedBy = model.Id;

                        _context.Update(userExist);
                        success = true;
                    }
                    else
                    {
                        var addUser = new User
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            Password = model.Password,
                            PhoneNumber = model.PhoneNumber,
                            CreatedDate = DateTime.Now,
                            PasswordExpiryDate = DateTime.Now.AddMonths(6),
                            Deleted = false
                        };
                        _context.Users.Add(addUser);
                        success = true;
                    }
                    _context.SaveChanges();
                    if (success)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return StatusCode(500);
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private async Task<bool> GenerateTicket(UserModel user)
        {
            try
            {
                var claims = new List<Claim> {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.NameIdentifier, user.FirstName),
                            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMonths(1),
                    AllowRefresh = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
