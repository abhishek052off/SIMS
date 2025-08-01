using SIMSWeb.Business.IService;
using SIMSWeb.Business.ServiceDTO.Course;
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

        public async Task DeleteCourse(int id)
        {
            var course = await GetCourseById(id);
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

        public async Task UpdateCourse(UpdateCourseDTO course)
        {
            var _course = new Course
            {
                Id = course.Id,
                Name = course.Name,
                IsActive = course.IsActive,
            };

            await _courseRepository.UpdateCourse(_course);
        }

        public async Task<int> GetCourseCount(string searchText)
        {
            var count = await _courseRepository.GetCourseCount(searchText);
            return count;
        }
    }
}
