using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.Service;
using SIMSWeb.ConstantsAndUtilities;
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
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, 
            UserSession session, ICourseService courseService, ITeacherService teacherService,
            IStudentService studentService)
        {
            _logger = logger;
            _userService = userService;
            _session = session;
            _courseService = courseService;
            _teacherService = teacherService;
            _studentService = studentService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Profile()
        {

            var adminProfile = new AdminProfile();

            if (string.IsNullOrEmpty(_session.Email))
            {
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

            adminProfile.RecentEnrollments = new List<RecentEnrollment>();
            var enrollments = await _studentService.GetRecentEnrolledStudents();

            var recentEnrollments = enrollments.Select(e => new RecentEnrollment
            {
                EnrollmentDate = e.Student.EnrollmentDate,
                Name = e.Student.User.Name,
                CourseName = e.Course.Name,
            }).ToList();

            adminProfile.RecentEnrollments = recentEnrollments;

            var assignmentsDueIn7Days = await _courseService.GetCourseDueInSevenDays(_session.Role, null);

            var assignmentsDueIn7DaysInfo = assignmentsDueIn7Days.Select(a => new PendingAssignments
            {
                CourseName = a.Name,
                AssignmentStatus = true
            }).ToList();

            adminProfile.PendingAssignments = assignmentsDueIn7DaysInfo;

            return View(adminProfile);
        }


        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> TeacherProfile()
        {
            var teacherProfile = new TeacherProfile();

            if (string.IsNullOrEmpty(_session.Email))
            {
                return View(teacherProfile);
            }              

            teacherProfile.User = new User();
            var user = await _userService.GetUserByEmail(_session.Email);
            teacherProfile.User = user;

            teacherProfile.RecentlyEnrolledStudents = new List<RecentlyEnrolledStudents>();
            var enrollments = await _studentService.GetStudentEnrolledByUserId(_session.Id);

            var recentEnrollments = enrollments.Select(e => new RecentlyEnrolledStudents
            {
                EnrollmentDate = e.Student.EnrollmentDate,
                Name = e.Student.User.Name,
                CourseName = e.Course.Name,
            }).ToList();

            teacherProfile.RecentlyEnrolledStudents = recentEnrollments;            

            var coursesByTeacher = await _courseService.GetCoursesByUserId(_session.Id, 0, string.Empty, 0, 0);

            var coursesByTeacherInfo = coursesByTeacher.Select(c => new TeacherCourses
            {
                CourseName = c.Name,
                StudentsEnrolled = c.Enrollments.Select(e => e.Student.User.Name).ToList(),
                AssignmentCreated = c.Assignments.Select(a => a.Title).ToList(),
            }).ToList();

            teacherProfile.TeacherCourses = coursesByTeacherInfo;

            teacherProfile.TeacherProfileMetrics = new TeacherProfileMetrics();

            teacherProfile.TeacherProfileMetrics.ActiveCourses = coursesByTeacher.Count;
            teacherProfile.TeacherProfileMetrics.TotalStudentsTaught = enrollments.Count;
            teacherProfile.TeacherProfileMetrics.TotalAssignmentsCreated = coursesByTeacher.Sum(c =>
                c.Assignments.Count);

            return View(teacherProfile);
        }       
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
