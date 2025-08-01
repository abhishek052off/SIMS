using SIMSWeb.Business.ServiceDTO.Course;
using SIMSWeb.Model.Models;
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
        Task AddCourse(Course course);
        Task UpdateCourse(UpdateCourseDTO course);
        Task DeleteCourse(Course course);
        Task<Course> GetCourseById(int id);
        Task<int> GetCourseCount(string searchText);
    }
}
