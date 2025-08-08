using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Data.IRepository
{
    public interface ISubmissionRepository
    {
        Task<List<Submission>> GetSubmissionByAssignmentId(int assignmentId);
        Task<Submission> GetSubmissionById(int id);
        Task AddSubmission(Submission submission);
        Task UpdateSubmission(Submission submission);
        // Task DeleteAssignment(Assignment assignment);
    }
}
