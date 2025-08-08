using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.Service;
using SIMSWeb.Business.ServiceDTO.Course;
using SIMSWeb.Business.ServiceDTO.Student;
using SIMSWeb.Business.ServiceDTO.Teacher;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using SIMSWeb.Models;
using SIMSWeb.Models.Course;
using System.Drawing.Printing;
using System.Security.Claims;

namespace SIMSWeb.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService,
            ITeacherService teacherService, IStudentService studentService,
            IMapper mapper)
        {
            _courseService = courseService;
            _teacherService = teacherService;
            _studentService = studentService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Courses")]
        public async Task<ActionResult> ManageCourses(int TeacherFilter,
            string CourseSearchText, int Page = 1, int PageSize = 5)
        {
            var manageCourseVM = new ManageCourseVM();

            // Calculate the skip and take for pagination
            var skip = (Page - 1) * PageSize;

            var teachersList = await GetTeacherList();

            manageCourseVM.FilterModel = new CourseFilterModel();
            manageCourseVM.FilterModel.TeacherList = teachersList;


            ViewBag.TeacherId = TeacherFilter == 0 ? -1 : TeacherFilter;
            ViewBag.CourseSearchText = CourseSearchText ?? String.Empty;

            if (User.IsInRole("Teacher") || User.IsInRole("Student"))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var courseListForTeacher = await _courseService
                    .GetCoursesByUserId(Convert.ToInt32(userId), TeacherFilter,
                    CourseSearchText, skip, PageSize);

                var totalCoursesOfTeacher = await _courseService.GetCourseCount(TeacherFilter, CourseSearchText);

                manageCourseVM.Courses = courseListForTeacher.Select(c => new ManageCourseModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsActive = c.IsActive,
                    Teacher = c.Teacher,
                    Description = c.Description

                }).ToList();

                manageCourseVM.Paginations = new PaginatedResult<ManageCourseModel>
                {
                    Items = manageCourseVM.Courses,
                    TotalRecords = totalCoursesOfTeacher,
                    PageSize = PageSize,
                    CurrentPage = Page
                };


                return View(manageCourseVM);

            }



            // Get the total number of records
            var totalRecords = await _courseService.GetCourseCount(TeacherFilter, CourseSearchText);

            var courses = await _courseService.GetCourses(TeacherFilter, CourseSearchText,
                skip, PageSize);
            manageCourseVM.Courses = courses.Select(c => new ManageCourseModel
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive,
                Teacher = c.Teacher,
                Description = c.Description
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

        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> AddCourses()
        {
            var courseVM = new AddCourseVM();

            courseVM.TeacherListModel = await GetTeacherList();

            return View(courseVM);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> AddCourses(CourseViewModel courseRequest)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(AddCourses));
            }

            var course = _mapper.Map<CourseViewModel>(courseRequest);

            await _courseService.AddCourse(course);
            return RedirectToAction("ManageCourses");

        }

        [Authorize(Policy = "AdminTeacherOnly")]
        public async Task<ActionResult> EditCourse(int id, bool modifyTeacher, bool enrollStudents)
        {
            var courseVM = new UpdateCourseVM();


            var course = await _courseService.GetCourseDetailsById(id);
            if (course == null)
            {
                return RedirectToAction("ManageCourses");
            }

            courseVM.Course = _mapper.Map<CourseViewModel>(course);
            courseVM.Course.Department = course?.Teacher?.Department ?? "";
            courseVM.Course.ModifyTeacher = modifyTeacher;
            courseVM.Course.EnrollStudents = enrollStudents;

            // Map teacher list for Admin and Teacher view
            if (!enrollStudents)
            {
                var teacherList = await GetTeacherList();
                courseVM.TeachersList = teacherList;
            }
            else
            {
                var studentList = await _studentService.GetStudentsListByCourseId(id);
                courseVM.StudentList = studentList;
            }

            return View(courseVM);
        }

        [HttpPost]
        [Authorize(Policy = "AdminTeacherOnly")]
        public async Task<ActionResult> EditCourse(CourseViewModel courseRequest)
        {
            if (!ModelState.IsValid)
            {
                if (courseRequest.ModifyTeacher || courseRequest.EnrollStudents)
                {
                    var queryParams = new Dictionary<string, object>();
                    queryParams["Id"] = courseRequest.Id;

                    if (courseRequest.ModifyTeacher)
                    {
                        queryParams["ModifyTeacher"] = true;
                    }
                    else if (courseRequest.EnrollStudents)
                    {
                        queryParams["EnrollStudents"] = true;
                    }
                    return RedirectToAction("ViewCourse", "Course", queryParams);
                }

                return RedirectToAction("EditCourse", "Course",
                    new { Id = courseRequest.Id });
            }

            if (courseRequest.EnrollStudents)
            {
                if (courseRequest?.StudentId == null)
                {
                    return RedirectToAction("ViewCourse", "Course",
                        new { Id = courseRequest.Id });
                }

                var enrollStudent = new EnrollmentViewModel
                {
                    CourseId = courseRequest.Id,
                    StudentId = (int)courseRequest.StudentId,
                    Comments = courseRequest?.Comments ?? "",
                    Marks = courseRequest?.Marks ?? 0,
                    Term = (int)courseRequest.Term,

                };

                await _studentService.EnrollStudents(enrollStudent);
                TempData["success"] = "Student enrolled successfully";
                return RedirectToAction("ViewCourse", "Course", new { Id = courseRequest.Id });
            }

            await _courseService.UpdateCourse(courseRequest);
            if (courseRequest.ModifyTeacher)
            {
                if (courseRequest?.TeacherId == null)
                {
                    return RedirectToAction("ViewCourse", "Course", new { Id = courseRequest.Id });
                }

                var teacher = new TeacherViewModel
                {
                    Id = (int)courseRequest.TeacherId,
                    Department = courseRequest.Department,
                    HireDate = DateTime.Now

                };
                await _teacherService.UpdateTeacher(teacher);
                TempData["success"] = "Teacher updated successfully";
                return RedirectToAction("ViewCourse", "Course", new { Id = courseRequest.Id });
            }

            TempData["success"] = "Course updated successfully";
            return RedirectToAction("ManageCourses");
        }

        [Authorize(Policy = "AdminOnly")]
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
        [Authorize(Policy = "AdminOnly")]
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

        public async Task<ActionResult> ViewCourse(int id)
        {
            var course = await _courseService.GetCourseDetailsById(id);

            var isStudent = User.IsInRole("Student");

            var viewModel = new ViewCourseModel
            {
                Id = course.Id,
                Name = course.Name,
                IsActive = course.IsActive,
                TeacherId = course.TeacherId,
                Description = course.Description,
                TeacherName = course?.Teacher?.User?.Name ?? "",
                Department = course?.Teacher?.Department ?? "",
                TeacherHireDate = course?.Teacher?.HireDate,
                AssignmentsCount = course?.Assignments?.Count ?? 0,
                Students = course.Enrollments.Select(e => new StudentViewDTO
                {
                    StudentName = e.Student.User.Name,
                    EnrollmentDate = e.Student.EnrollmentDate,
                    Term = e.Term,
                    Comments = e.Comments,
                    Marks = e.Marks,
                    UserId = e.Student.UserId

                }).ToList()
            };

            if (isStudent)
            {
                var studentUserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (studentUserId > 0)
                {
                    viewModel.Students = viewModel.Students
                        .Where(s => s.UserId == studentUserId).ToList();
                }

            }

            return View(viewModel);
        }

    }
}