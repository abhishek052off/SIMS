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
        Task<List<Student>> GetStudents(int courseId);
        Task AddStudent(StudentViewModel student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(Student student);
        Task EnrollStudents(EnrollmentViewModel enrolledDetails);
    }
}
