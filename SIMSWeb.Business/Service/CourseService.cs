using SIMSWeb.Business.IService;
using SIMSWeb.Data.IRepository;
using SIMSWeb.Data.Repository;
using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task AddCourse(Course course)
        {
            await _courseRepository.AddCourse(course);
        }

        public async Task DeleteCourse(Course course)
        {
            await _courseRepository.DeleteCourse(course);
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _courseRepository.GetCourseById(id);
        }

        public async Task<List<Course>> GetCourses(string courseSearchText, int skip, int pageSize)
        {
            return await _courseRepository.GetCourses(courseSearchText, skip, pageSize);
        }

        public async Task UpdateCourse(Course course)
        {
            await _courseRepository.UpdateCourse(course);
        }

        public async Task<int> GetCourseCount(string searchText)
        {
            var count = await _courseRepository.GetCourseCount(searchText);
            return count;
        }
    }
}
