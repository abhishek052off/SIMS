using SIMSWeb.Business.ServiceDTO.Course;
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
        Task<List<Course>> GetCourses(string courseSearchText, int skip, int pageSize);
        Task<List<Course>> GetCoursesByUserId(int userId, string courseSearchText, int skip, int pageSize);
        Task AddCourse(CourseViewModel course);
        Task UpdateCourse(CourseViewModel course);
        Task DeleteCourse(int id);
        Task<Course> GetCourseById(int id);
        Task<Course> GetCourseDetailsById(int id);
        Task<int> GetCourseCount(string searchText);

    }
}
