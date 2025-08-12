using SIMSWeb.Business.ServiceDTO.CourseDTO;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.IService
{
    public interface ICourseService
    {
        Task<List<Course>> GetCourses(int TeacherFilter, string courseSearchText, int skip, int pageSize);
        Task<List<Course>> GetCoursesByUserId(int userId, int teacherFilter, string courseSearchText, int skip, int pageSize);
        Task AddCourse(CourseViewModel course);
        Task UpdateCourse(CourseViewModel course);
        Task DeleteCourse(int id);
        Task<Course> GetCourseById(int id);
        Task<Course> GetCourseDetailsById(int id);
        Task<int> GetCourseCount(int teacherFilter, string searchText);
        Task<int> GetActiveCoursesCount();
        Task<double> AverageClassSize();
        Task<List<Course>> GetActiveCourseofRole(int userId);
        Task<List<Course>> GetCourseDueInSevenDays(string role, int? userId);
    }
}
