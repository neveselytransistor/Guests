using System.Collections.Generic;
using System.Threading.Tasks;
using Guests.Models;
using Guests.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Guests.Controllers
{
    [Authorize]
    public class GuestController : Controller
    {
        private readonly IUserService _userService;

        public GuestController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ViewResult> List(string search, UserState state = UserState.None)
        {
            ViewBag.Guests = await _userService.SearchUsers(search, state);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateState([FromBody] UpdateStateRequest data)
        {
            await _userService.UpdateUserState(data.UserId, data.UserState);

            return Ok();
        }

        public async Task<IActionResult> Finish()
        {
            await _userService.Finish();

            return Ok();
        }
    }

    public class UpdateStateRequest
    {
        public int UserId { get; set; }

        public UserState UserState { get; set; }
    }
}