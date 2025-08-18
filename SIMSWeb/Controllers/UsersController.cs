using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIMSWeb.Business.IService;
using SIMSWeb.ConstantsAndUtilities;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using SIMSWeb.Models;
using SIMSWeb.Models.User;
using System.Drawing.Printing;

namespace SIMSWeb.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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

            ViewBag.UserSearchText = SearchText ?? string.Empty;

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
            }, "Value", "Text", UserRole);            

            manageUsersVM.Paginations = new PaginatedResult<ManageUsersModel>
            {
                Items = manageUsersVM.Users,
                TotalRecords = totalRecords,
                PageSize = PageSize,
                CurrentPage = Page
            };
            return View(manageUsersVM);
        }

        [Authorize(Policy = "AdminOnly")]
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
                TempData["success"] = "User added successfully";
                return RedirectToAction("ManageUsers");
            }
            return View();
        }

        public async Task<ActionResult<UserViewModel>> EditUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("ManageUsers");
            }

            var _user = _mapper.Map<UserViewModel>(user);
            return View(_user);
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(UserViewModel userRequest)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUser(userRequest);
                TempData["success"] = "User updated successfully";
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
                TempData["success"] = "User deleted successfully";
                return RedirectToAction("ManageUsers");

            }
            return View();
        }
    }
}
