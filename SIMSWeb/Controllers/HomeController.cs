using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.Service;
using SIMSWeb.ConstantsAndUtilities;
using SIMSWeb.ConstantsAndUtilities.CourseUtilities;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using SIMSWeb.Models;
using System.Diagnostics;

namespace SIMSWeb.Controllers
{

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

        public async Task<ActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Profile", "Home");
            }

            if (User.IsInRole("Teacher"))
            {
                return RedirectToAction("TeacherProfile", "Home");
            }

            return RedirectToAction("StudentProfile", "Home");
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
                Id = c.Id,
                CourseName = c.Name,
                StudentsEnrolled = c.Enrollments.Select(e => e.Student.User.Name).ToList(),
                AssignmentCreated = c.Assignments.Select(a => new AssignmentList
                {
                    AssignmentId = a.Id,
                    AssignmentName = a.Title
                }).ToList(),
            }).ToList();

            teacherProfile.TeacherCourses = coursesByTeacherInfo;

            teacherProfile.TeacherProfileMetrics = new TeacherProfileMetrics();

            teacherProfile.TeacherProfileMetrics.ActiveCourses = coursesByTeacher.Count;
            teacherProfile.TeacherProfileMetrics.TotalStudentsTaught = enrollments.Count;
            teacherProfile.TeacherProfileMetrics.TotalAssignmentsCreated = coursesByTeacher.Sum(c =>
                c.Assignments.Count);

            return View(teacherProfile);
        }

        [Authorize(Roles = "Student")]
        public async Task<ActionResult> StudentProfile()
        {
            var studentProfile = new StudentProfile();

            if (string.IsNullOrEmpty(_session.Email))
            {
                return View(studentProfile);
            }

            studentProfile.User = new User();
            var user = await _userService.GetUserByEmail(_session.Email);
            studentProfile.User = user;

            var enrolledCourses = await _courseService.GetCoursesByUserId(_session.Id, 0, string.Empty, 0, 0);

            var enrolledCoursesInfo = enrolledCourses.Select(c => new EnrolledCourses
            {
                Id = c.Id,
                CourseName = c.Name,
                AssignmentCreated = c.Assignments.Select(a => a.Title).ToList(),
            }).ToList();

            studentProfile.EnrolledCourses = enrolledCoursesInfo;

            studentProfile.StudentProfileMetrics = new StudentProfileMetrics();

            studentProfile.StudentProfileMetrics.ActiveCourses = enrolledCourses.Count;

            studentProfile.StudentProfileMetrics.ActiveAssignments = enrolledCoursesInfo
                .Select(e => e.AssignmentCreated.Count).FirstOrDefault();

            studentProfile.StudentProfileMetrics.CompletedAssignmentsCount = enrolledCourses
            .SelectMany(course => course.Assignments)
            .SelectMany(assignment => assignment.Submissions)
            .Where(submission => submission.Student.UserId == _session.Id)
            .Count(submission => submission.SubmittedAt != null);

            studentProfile.StudentProfileMetrics.MaximumMarks = enrolledCourses
                .SelectMany(course => course.Assignments)
                .SelectMany(assignment => assignment.Submissions)
                .Where(submission => submission.Student.UserId == _session.Id)
                .Max(submission => submission.Score);

            var studentProgress = await _courseService.GetProgressofStudent(_session.Id);

            studentProfile.AssignmentProgress = studentProgress;

            return View(studentProfile);
        }


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public IActionResult Error(int statusCode)
        {
            var _statusCode = statusCode > 0 ? statusCode : HttpContext.Response.StatusCode;

            var model = new ErrorViewModel
            {
                StatusCode = _statusCode
            };

            model.ErrorMessage = statusCode switch
            {
                404 => "Oops! The page you requested could not be found.",
                500 => "Something went wrong on our server. Please try again later.",
                403 => "You don't have permission to view this page.",
                400 => "The request is invalid. Please check and try again.",
                502 => "There seems to be a problem with the server.",
                503 => "The service is temporarily unavailable. Please try again later.",
                _ => "An unexpected error occurred. Please try again later."
            };

            return View(model);
        }
    }
}
