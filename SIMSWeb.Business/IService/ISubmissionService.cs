using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.IService
{
    public interface ISubmissionService
    {
        Task<List<Submission>> GetSubmissionByAssignmentId(int assignmentId);
        Task<Submission> GetSubmissionById(int id);
        Task AddSubmission(SubmissionViewModel submission);
        Task UpdateSubmission(Submission submission);
        // Task DeleteAssignment(Assignment assignment);
    }
}
