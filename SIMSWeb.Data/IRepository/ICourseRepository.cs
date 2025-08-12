using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Data.IRepository
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetCourses(int TeacherFilter, string courseSearchText, int skip, int pageSize);
        Task AddCourse(Course course);
        Task UpdateCourse(Course course);
        Task DeleteCourse(Course course);
        Task<Course> GetCourseById(int id);
        Task<int> GetCourseCount(int teacherFilter, string searchText);
        Task<Course> GetCourseDetailsById(int id);
        Task<List<Course>> GetCoursesByUserId(int userId, int teacherFilter, string courseSearchText, int skip, int pageSize);
        Task<int> GetActiveCoursesCount();
        Task<double> AverageClassSize();
        Task<List<Course>> GetActiveCourseofRole(int userId);
        Task<List<Course>> GetCourseDueInSevenDays(string role, int? userId);
    }
}
