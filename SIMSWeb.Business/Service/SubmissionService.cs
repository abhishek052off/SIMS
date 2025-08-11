using AutoMapper;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.ServiceDTO.SubmissionDTO;
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
        private readonly IMapper _mapper;

        public SubmissionService(ISubmissionRepository submissionRepository, IMapper mapper)
        {
            _submissionRepository = submissionRepository;
            _mapper = mapper;
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

        public async Task UpdateSubmission(SubmissionViewModel submissionInfo)
        {
            var submission = _mapper.Map<Submission>(submissionInfo);
            await _submissionRepository.UpdateSubmission(submission);
        }
    }
}
