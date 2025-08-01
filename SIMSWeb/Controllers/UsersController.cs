using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.ServiceDTO.User;
using SIMSWeb.ConstantsAndUtilities;
using SIMSWeb.Model.Models;
using SIMSWeb.Models;
using SIMSWeb.Models.User;
using System.Drawing.Printing;

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

        public async Task<ActionResult> ManageUsers(string UserRole, string SearchText, int Page = 1, int PageSize = 10)
        {
            var manageUsersVM = new ManageUserVM();

            // Calculate the skip and take for pagination
            var skip = (Page - 1) * PageSize;

            // Get the total number of records
            var totalRecords = await _userService.GetUserCount(UserRole, SearchText);

            var users = await _userService.GetUsers(UserRole, SearchText, skip, PageSize);
            manageUsersVM.Users = users.Select(u => new ManageUsersModel
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role,
            }).ToList();

            manageUsersVM.Filters = new FilterModel();
            manageUsersVM.Filters.Roles = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = UsersConstants.ADMIN_ROLE,
                    Value = UsersConstants.ADMIN_ROLE
                },
                new SelectListItem
                {
                    Text = UsersConstants.TEACHER_ROLE,
                    Value = UsersConstants.TEACHER_ROLE
                },
                new SelectListItem
                {
                    Text = UsersConstants.STUDENT_ROLE,
                    Value = UsersConstants.STUDENT_ROLE
                }
            }, "Value", "Text");

            manageUsersVM.Paginations = new PaginatedResult<ManageUsersModel>
            {
                Items = manageUsersVM.Users,
                TotalRecords = totalRecords,
                PageSize = PageSize,
                CurrentPage = Page
            };
            return View(manageUsersVM);
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

        public async Task<ActionResult<UpdateUserDTO>> EditUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("ManageUsers");
            }

            var _user = new UpdateUserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                Name = user.Name,
            };
            return View(_user);
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(UpdateUserDTO userRequest)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUser(userRequest);
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("ManageUsers");
            }
            return View();
        }

        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserById(id);

            var viewModel = new DeleteUserModel
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
            };

            return View(viewModel);
        }
        [HttpPost, ActionName("DeleteUser")]
        public async Task<ActionResult> DeletePOST(int id)
        {
            if (ModelState.IsValid)
            {
                await _userService.DeleteUser(id);
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("ManageUsers");

            }
            return View();
        }
    }
}
