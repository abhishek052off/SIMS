using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Data.IRepository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudents(int courseId);
        Task<List<Student>> GetEnrolledStudentsByCourseId(int courseId);
        Task<List<Enrollment>> GetRecentEnrolledStudents();
        Task AddStudent(Student student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(Student student);
        Task<Student> GetStudentById(int id);
        Task<Student> GetStudentByUserId(int id);
        Task EnrollStudents(Enrollment enrolledDetails);
        Task<List<Enrollment>> GetStudentEnrolledByUserId(int userId);
    }
}
