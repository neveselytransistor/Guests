using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Guests.Models;
using Guests.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Guests.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromForm] RegistrationData registrationData)
        {
            await _userService.RegisterNewUser(new UserInfo
            {
                Name = registrationData.Name,
                Email = registrationData.Email,
                Phone = registrationData.Phone,
                State = UserState.None
            });

            return Redirect("/Auth/Success");
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginData data)
        {
            var user = await _userService.GetSystemUserOrDefault(data.Login, data.Password);

            if (user == null)
            {
                // TODO: Выводить форму с ошибками
                throw new Exception("Пользователь не найден");
            }

            var claims = new[]
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };
            var claimIdentity = new ClaimsIdentity(claims, "ApplicationCookie");
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Redirect("/Guest/List");
        }
    }

    public class RegistrationData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class LoginData
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}