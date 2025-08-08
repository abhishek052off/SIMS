using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.IService
{
    public interface IAssignmentService
    {
        Task<List<Assignment>> GetAssignments(int courseId);
        Task AddAssignment(AssignmentViewModel assignment);
        Task UpdateAssignment(Assignment assignment);
        // Task DeleteStudent(Student student);
    }
}
