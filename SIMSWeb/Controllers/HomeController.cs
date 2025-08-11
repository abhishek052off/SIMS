using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.Service;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using SIMSWeb.Models;

namespace SIMSWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly UserSession _session;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, 
            UserSession session, ICourseService courseService, ITeacherService teacherService  )
        {
            _logger = logger;
            _userService = userService;
            _session = session;
            _courseService = courseService;
            _teacherService = teacherService;
        }
        
        public async Task<ActionResult> Profile()
        {
            var adminProfile = new AdminProfile();

            if (string.IsNullOrEmpty(_session.Email)) {
                return View(adminProfile);                
            }
            
            adminProfile.Status = new ProfileStatus();

            var userCount = await _userService.GetUserCount(string.Empty, string.Empty);
            var coursesCount = await _courseService.GetActiveCoursesCount();
            var avgSizeClass = await _courseService.AverageClassSize();

            adminProfile.Status.TotalUsers = userCount;
            adminProfile.Status.ActiveCourses = coursesCount;
            adminProfile.Status.AvgClassSizePerCourse = avgSizeClass;

            adminProfile.User = new User();
            var user = await _userService.GetUserByEmail(_session.Email);
            adminProfile.User = user;

            return View(adminProfile);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
