using Microsoft.EntityFrameworkCore;
using SIMSWeb.Business.Service;
using SIMSWeb.Data.Context;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Data.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly SIMSDBContext _dbContext;

        public AssignmentRepository(SIMSDBContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public async Task AddAssignment(Assignment assignment)
        {
           _dbContext.Assignments.Add(assignment);
           await _dbContext.SaveChangesAsync();
        }

        public async Task<Assignment> GetAssignmentById(int id)
        {
            var assignment = await _dbContext.Assignments
                .Include(x => x.Course)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return assignment;
        }

        public async Task<List<Assignment>> GetAssignments(int courseId)
        {
            var assignments = await _dbContext.Assignments
                .Include(x => x.Course)
                .Where(x => x.CourseId == courseId)
                .ToListAsync();

            return assignments;

        }

        public async Task UpdateAssignment(Assignment assignment)
        {
            _dbContext.Assignments.Update(assignment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
