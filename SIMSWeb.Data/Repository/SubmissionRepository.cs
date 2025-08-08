using Microsoft.EntityFrameworkCore;
using SIMSWeb.Data.Context;
using SIMSWeb.Data.IRepository;
using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Data.Repository
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly SIMSDBContext _dbContext;

        public SubmissionRepository(SIMSDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddSubmission(Submission submission)
        {
            _dbContext.Submissions.Add(submission);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Submission>> GetSubmissionByAssignmentId(int assignmentId)
        {
            var submissions = await _dbContext.Submissions
                .Include(x => x.Student)
                    .ThenInclude(s => s.User)
                .Include(x => x.Assignment)
                .Where(x => x.AssignmentId == assignmentId)
                .ToListAsync();

            return submissions;
        }

        public async Task<Submission> GetSubmissionById(int id)
        {
            var submission = await _dbContext.Submissions
                .Include(x => x.Student)
                    .ThenInclude(s => s.User)
                .Include(x => x.Assignment)
                .Where(x => x.AssignmentId == id)
                .FirstOrDefaultAsync();

            return submission;
        }

        public async Task UpdateSubmission(Submission submission)
        {
            _dbContext.Submissions.Update(submission);
            await _dbContext.SaveChangesAsync();
        }
    }
}
