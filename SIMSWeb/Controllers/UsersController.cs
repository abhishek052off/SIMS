using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIMSWeb.Business.IService;
using SIMSWeb.Model.Models;
using SIMSWeb.Models;

namespace SIMSWeb.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;   
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ManageUsers()
        {
            var users = await _userService.GetUsers();
            return View(users);
        }

        public IActionResult AddUsers()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUsers(AddUserModel userRequest)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = userRequest.Name,
                    Email = userRequest.Email,
                    Password = userRequest.Password,
                    Role = userRequest.Role,
                    CreatedAt = DateTime.Now,
                };

                await _userService.AddUser(user);
                return RedirectToAction("ManageUsers");
            }
            return View();
        }

        public async Task<ActionResult<User>> EditUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("ManageUsers");
            }

            return View(user);
        }

        
    }
}
