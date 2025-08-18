using AspNetCoreGeneratedDocument;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.Service;
using SIMSWeb.Business.ServiceDTO.StudentDTO;
using SIMSWeb.Business.ServiceDTO.SubmissionDTO;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;

namespace SIMSWeb.Controllers
{
    [Authorize]
    public class SubmissionController : Controller
    {
        private readonly ISubmissionService _submissionService;
        private readonly IStudentService _studentService;
        private readonly IAssignmentService _assignmentService;
        private readonly IMapper _mapper;

        public SubmissionController(ISubmissionService submissionService, IMapper mapper,
            IStudentService studentService, IAssignmentService assignmentService)
        {
            _submissionService = submissionService;
            _mapper = mapper;
            _studentService = studentService;
            _assignmentService = assignmentService;
        }

        public async Task<ActionResult> ViewSubmissions(int assignmentId, int courseId)
        {
            var submissions = await _submissionService.GetSubmissionByAssignmentId(assignmentId);

            ViewBag.CourseId = courseId == 0 ? 0 : courseId;
            ViewBag.AssignmentId = assignmentId == 0 ? 0 : assignmentId;

            var submissionVM = submissions.Select(a => new SubmissionViewModel
            {
                Id = a.Id,
                StudentId = a.StudentId,
                StudentName = a.Student.User.Name,
                AssignmentId = a.AssignmentId,
                Feedback = a.Feedback,
                Score = a.Score,
                SubmittedAt = a.SubmittedAt,
            }).ToList();

            return View(submissionVM);

        }

        [Authorize(Policy = "AdminTeacherOnly")]
        public async Task<ActionResult> AddSubmission(int assignmentId, int courseId)
        {
            var submissionVM = new UpdateSubmissionDTO();
            var assignment = await _assignmentService.GetAssignmentById(assignmentId);

            ViewBag.CourseId = courseId == 0 ? 0 : courseId;
            ViewBag.AssignmentId = assignmentId == 0 ? 0 : assignmentId;

            var studentList = await _studentService.GetEnrolledStudentsByCourseId(courseId);
            var submissions = await _submissionService.GetSubmissionByAssignmentId(assignmentId);

            var studentsHasNoSubmission = studentList.Where(s => 
                !submissions.Any(sub => sub.Student.User.Name == s.Name)).ToList();

            submissionVM.StudentList = studentsHasNoSubmission;
            submissionVM.Submission = new SubmissionViewModel();
            submissionVM.Submission.AssignmentId = assignmentId;
            submissionVM.Submission.AssignmentName = assignment.Title;
            submissionVM.Submission.SubmittedAt = DateTime.Now;

            //submissionVM.Submission = new SubmissionViewModel
            //{
            //    Id = a.Id,
            //    StudentId = a.StudentId,
            //    AssignmentId = a.AssignmentId,
            //    Feedback = a.Feedback,
            //    Score = a.Score,
            //    SubmittedAt = a.SubmittedAt,
            //};

            return View(submissionVM);

        }

        [HttpPost]
        [Authorize(Policy = "AdminTeacherOnly")]
        public async Task<ActionResult> AddSubmission(int assignmentId, int courseId, SubmissionViewModel submissionRequest)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewSubmissions", 
                    new { courseId = courseId, assignmentId = assignmentId });
            }

            ViewBag.CourseId = courseId == 0 ? 0 : courseId;
            ViewBag.AssignmentId = assignmentId == 0 ? 0 : assignmentId;

            var addedSubmission = _mapper.Map<SubmissionViewModel>(submissionRequest);

            await _submissionService.AddSubmission(addedSubmission);
            return RedirectToAction("ViewSubmissions",
                   new { courseId = courseId, assignmentId = assignmentId });

        }

        [Authorize(Policy = "AdminTeacherOnly")]
        public async Task<ActionResult> EditSubmission(int id, int courseId, int assignmentId)
        {
            ViewBag.CourseId = courseId == 0 ? 0 : courseId;
            ViewBag.AssignmentId = assignmentId == 0 ? 0 : assignmentId;

            var assignment = await _assignmentService.GetAssignmentById(assignmentId);


            var submission = await _submissionService.GetSubmissionById(id);
            if (submission == null)
            {
                return RedirectToAction("ViewSubmissions",
                   new { courseId = courseId, assignmentId = assignmentId });
            }

            var submissionView = _mapper.Map<SubmissionViewModel>(submission);
            
            if(submissionView.StudentId != 0)
            {
                var student = await _studentService.GetStudentById(submissionView.StudentId);
                submissionView.StudentName = student.User.Name;
            }

            submissionView.AssignmentId = assignment.Id;
            submissionView.AssignmentName = assignment.Title;
            submissionView.StudentId = submission.StudentId;

            return View(submissionView);
        }

        [HttpPost]
        [Authorize(Policy = "AdminTeacherOnly")]
        public async Task<ActionResult> EditSubmission(int courseId, int id, SubmissionViewModel submissionRequest)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewSubmissions",
                   new { courseId = courseId, assignmentId = id });
            }


            await _submissionService.UpdateSubmission(submissionRequest);

            TempData["success"] = "Submission updated successfully";
            return RedirectToAction("ViewSubmissions",
                   new { courseId = courseId, assignmentId = submissionRequest.AssignmentId });
        }

    }
}
