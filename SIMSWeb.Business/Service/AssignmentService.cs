using Microsoft.AspNetCore.Http;
using SIMSWeb.Business.IService;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.Service
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task AddAssignment(AssignmentViewModel assignment)
        {
            var _assignment = new Assignment
            {
                CourseId = assignment.CourseId,
                Title = assignment.Title,
                MaxScore = assignment.MaxScore,
                Description = assignment.Description,
                DueDate = Convert.ToDateTime(assignment.DueDate),
            };

           await _assignmentRepository.AddAssignment(_assignment);
        }

        public async Task<List<Assignment>> GetAssignments(int courseId)
        {
            return await _assignmentRepository.GetAssignments(courseId);
        }

        public async Task UpdateAssignment(Assignment assignment)
        {
            await _assignmentRepository.UpdateAssignment(assignment);
        }
    }
}
