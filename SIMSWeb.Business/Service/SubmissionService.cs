using SIMSWeb.Business.IService;
using SIMSWeb.Data.IRepository;
using SIMSWeb.Data.Repository;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.Service
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository _submissionRepository;

        public SubmissionService(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public async Task AddSubmission(SubmissionViewModel submission)
        {
            var _submission = new Submission
            {
                StudentId = submission.StudentId,
                AssignmentId = submission.AssignmentId,
                Score = submission.Score,                
                Feedback = submission.Feedback,
                SubmittedAt = Convert.ToDateTime(submission.SubmittedAt),
            };

            await _submissionRepository.AddSubmission(_submission);
        }

        public async Task<List<Submission>> GetSubmissionByAssignmentId(int submissionId)
        {
            return await _submissionRepository.GetSubmissionByAssignmentId(submissionId);
        }

        public async Task<Submission> GetSubmissionById(int id)
        {
            return await _submissionRepository.GetSubmissionById(id);
        }

        public async Task UpdateSubmission(Submission submission)
        {
            await _submissionRepository.UpdateSubmission(submission);
        }
    }
}
