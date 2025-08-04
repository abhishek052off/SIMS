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
        Task<List<Course>> GetCourses(string courseSearchText, int skip, int pageSize);
        Task AddCourse(Course course);
        Task UpdateCourse(Course course);
        Task DeleteCourse(Course course);
        Task<Course> GetCourseById(int id);
        Task<int> GetCourseCount(string searchText);
        Task<Course> GetCourseDetailsById(int id);
    }
}
