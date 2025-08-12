using SIMSWeb.Business.ServiceDTO.StudentDTO;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.IService
{
    public interface IStudentService
    {
        Task<List<StudentSelect>> GetStudents(int courseId);
        Task<List<StudentSelect>> GetEnrolledStudentsByCourseId(int courseId);
        Task<List<Enrollment>> GetRecentEnrolledStudents();
        Task<Student> GetStudentById(int id);
        Task AddStudent(StudentViewModel student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(Student student);
        Task EnrollStudents(EnrollmentViewModel enrolledDetails);
        Task<List<Enrollment>> GetStudentEnrolledByUserId(int userId);
    }
}
