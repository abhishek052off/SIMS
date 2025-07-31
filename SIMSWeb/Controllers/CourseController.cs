using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.Service;
using SIMSWeb.ConstantsAndUtilities;
using SIMSWeb.Model.Models;
using SIMSWeb.Models;
using SIMSWeb.Models.Course;
using SIMSWeb.Models.User;
using System.Drawing.Printing;

namespace SIMSWeb.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ManageCourses(string CourseSearchText, int Page = 1, int PageSize = 2)
        {
            var manageCourseVM = new ManageCourseVM();

            // Calculate the skip and take for pagination
            var skip = (Page - 1) * PageSize;

            // Get the total number of records
            var totalRecords = await _courseService.GetCourseCount(CourseSearchText);

            var courses = await _courseService.GetCourses(CourseSearchText, skip, PageSize);
            manageCourseVM.Courses = courses.Select(c => new ManageCourseModel
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive,
            }).ToList();

            manageCourseVM.Paginations = new PaginatedResult<ManageCourseModel>
            {
                Items = manageCourseVM.Courses,
                TotalRecords = totalRecords,
                PageSize = PageSize,
                CurrentPage = Page
            };
            return View(manageCourseVM);
        }


        public IActionResult AddCourses()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddCourses(AddCourseModel courseRequest)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Name = courseRequest.Name,
                    IsActive= courseRequest.IsActive,
                };

                await _courseService.AddCourse(course);
                return RedirectToAction("ManageCourses");
            }
            return View();
        }
    }
}