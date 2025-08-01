using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.Service;
using SIMSWeb.Business.ServiceDTO.Course;
using SIMSWeb.Business.ServiceDTO.User;
using SIMSWeb.ConstantsAndUtilities;
using SIMSWeb.Model.Models;
using SIMSWeb.Models;
using SIMSWeb.Models.Course;
using SIMSWeb.Models.Teacher;
using SIMSWeb.Models.User;
using System.Drawing.Printing;

namespace SIMSWeb.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        public CourseController(ICourseService courseService, ITeacherService teacherService)
        {
            _courseService = courseService;
            _teacherService = teacherService;
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

        public async Task<List<TeacherSelect>> GetTeacherList()
        {
            var teachers = await _teacherService.GetTeachers();
            var teacherList = teachers.Select(u => new TeacherSelect
            {
                Id = u.Id,
                Name = u.User.Name,
            }).ToList();

            teacherList.Insert(0, new TeacherSelect
            {
                Id = -1,
                Name = "Select Teacher"
            });

            return teacherList;
        }

        public async Task<ActionResult> AddCourses()
        {
            var courseVM = new AddCourseVM();

            courseVM.TeacherListModel = await GetTeacherList();

            return View(courseVM);
        }

        [HttpPost]
        public async Task<ActionResult> AddCourses(CourseViewModel courseRequest)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(AddCourses));
            }

            var course = new Course // Entity leaking to Presentation layer, should be mapped to DTO
            {
                Name = courseRequest.Name,
                IsActive = courseRequest.IsActive,
                TeacherId = courseRequest.TeacherId
            };

            await _courseService.AddCourse(course);
            return RedirectToAction("ManageCourses");

        }

        public async Task<ActionResult> EditCourse(int id)
        {
            var courseVM = new UpdateCourseVM();
            var teacherList = await GetTeacherList();

            var course = await _courseService.GetCourseById(id);
            if (course == null)
            {
                return RedirectToAction("ManageCourses");
            }

            courseVM.Course = new UpdateCourseDTO
            {
                Id = course.Id,
                Name = course.Name,
                IsActive = course.IsActive,
                TeacherId = course.TeacherId,
            };

            courseVM.TeachersList = teacherList;
            return View(courseVM);
        }

        [HttpPost]
        public async Task<ActionResult> EditCourse(UpdateCourseDTO courseRequest)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EditCourse", "Course", new { Id = courseRequest.Id });
            }
            await _courseService.UpdateCourse(courseRequest);
            TempData["success"] = "Course updated successfully";
            return RedirectToAction("ManageCourses");
        }

        public async Task<ActionResult> DeleteCourse(int id)
        {
            var course = await _courseService.GetCourseById(id);

            var teacher = await _teacherService.GetTeacherById(id);

            var viewModel = new DeleteCourseModel
            {
                Name = course.Name,
                IsActive = course.IsActive,
                TeacherId = course.TeacherId,
                TeacherName = teacher.User.Name,
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("DeleteCourse")]
        public async Task<ActionResult> DeleteCoursePOST(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _courseService.DeleteCourse(id);
            TempData["success"] = "Course deleted successfully";
            return RedirectToAction("ManageCourses");



        }

    }
}