using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;

namespace SIMSWeb.Business.Service
{
    public interface IAssignmentRepository
    {
        Task<List<Assignment>> GetAssignments(int courseId);
        Task<Assignment> GetAssignmentById(int id);
        Task AddAssignment(Assignment assignment);
        Task UpdateAssignment(Assignment assignment);
    }
}