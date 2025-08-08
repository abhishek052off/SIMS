using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.Service;
using SIMSWeb.Business.ServiceDTO.Course;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using SIMSWeb.Models.Course;

namespace SIMSWeb.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IAssignmentService _assignmentService;
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public AssignmentController(IAssignmentService assignmentService, ICourseService courseService,
            IMapper mapper)
        {
            _assignmentService = assignmentService;
            _courseService = courseService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ViewAssignments(int CourseId)
        {
            var assignments = await _assignmentService.GetAssignments(CourseId);

            var assignmentsVM = assignments.Select(a => new AssignmentViewModel
            {
                Id = a.Id,
                CourseId = a.CourseId,
                CourseName = a.Course.Name,
                Title = a.Title,
                Description = a.Description,
                DueDate = a.DueDate,
                MaxScore = a.MaxScore
            }).ToList();

            ViewBag.CourseId = CourseId == 0 ? 0  : CourseId;

            return View(assignmentsVM);

        }

        public async Task<ActionResult> AddAssignment(int courseId)
        {
            var assignmentVM = new AssignmentViewModel();
            var course = await _courseService.GetCourseById(courseId);

            assignmentVM.CourseId = courseId;
            assignmentVM.CourseName = course.Name;
            assignmentVM.DueDate = DateTime.Today;


            return View(assignmentVM);
        }

        [HttpPost, ActionName("AddAssignment")]
        public async Task<ActionResult> AddAssignmentPost(AssignmentViewModel assignmentRequest)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewAssignments", new {courseId = assignmentRequest.CourseId });
            }

            var addedAssignment = _mapper.Map<AssignmentViewModel>(assignmentRequest);

            await _assignmentService.AddAssignment(addedAssignment);
            return RedirectToAction("ViewAssignments", new { courseId = assignmentRequest.CourseId });

        }

        [Authorize(Policy = "AdminTeacherOnly")]
        public async Task<ActionResult> EditAssignment(int id)
        {
            var assignmentView = new AssignmentViewModel();


            var assignment = await _assignmentService.GetAssignmentById(id);
            if (assignment == null)
            {
                return RedirectToAction("ViewAssignments", new {courseId = assignment.CourseId});
            }

            assignmentView = _mapper.Map<AssignmentViewModel>(assignment);           

            return View(assignmentView);
        }

        [HttpPost]
        [Authorize(Policy = "AdminTeacherOnly")]
        public async Task<ActionResult> EditAssignment(int courseId, AssignmentViewModel assignmentRequest)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewAssignments", new { courseId = courseId });
            }

            var assignmentView = _mapper.Map<Assignment>(assignmentRequest);
            assignmentView.CourseId = courseId;

            await _assignmentService.UpdateAssignment(assignmentView);

            TempData["success"] = "Assignment updated successfully";
            return RedirectToAction("ViewAssignments", new { courseId = courseId });
        }


    }
}
