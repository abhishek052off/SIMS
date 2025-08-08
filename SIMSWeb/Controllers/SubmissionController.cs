using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.Service;
using SIMSWeb.Business.ServiceDTO.Submission;
using SIMSWeb.Model.ViewModels;

namespace SIMSWeb.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly ISubmissionService _submissionService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public SubmissionController(ISubmissionService submissionService, IMapper mapper,
            IStudentService studentService)
        {
            _submissionService = submissionService;
            _mapper = mapper;
            _studentService = studentService;
        }

        public async Task<ActionResult> ViewSubmissions(int assignmentId, int courseId)
        {
            var submissions = await _submissionService.GetSubmissionByAssignmentId(assignmentId);

            ViewBag.CourseId = courseId;

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

        public async Task<ActionResult> AddSubmission(int assignmentId, int courseId)
        {
            var submissionVM = new UpdateSubmissionDTO();
            var submissions = await _submissionService.GetSubmissionByAssignmentId(assignmentId);

            var studentList = await _studentService.GetStudentsListByCourseId(courseId);

            submissionVM.StudentList = studentList;

            submissionVM.Submission = submissions.Select(a => new SubmissionViewModel
            {
                Id = a.Id,
                StudentId = a.StudentId,
                AssignmentId = a.AssignmentId,
                Feedback = a.Feedback,
                Score = a.Score,
                SubmittedAt = a.SubmittedAt,
            }).ToList();

            return View(submissionVM);

        }

    }
}
