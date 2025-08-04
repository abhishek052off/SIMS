using AutoMapper;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.ServiceDTO.Course;
using SIMSWeb.Data.IRepository;
using SIMSWeb.Data.Repository;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
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
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task AddCourse(CourseViewModel course)
        {
            var courseModel = _mapper.Map<Course>(course);
            await _courseRepository.AddCourse(courseModel);
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

        public async Task<Course> GetCourseDetailsById(int id)
        {
            return await _courseRepository.GetCourseDetailsById(id);
        }

        public async Task<List<Course>> GetCourses(string courseSearchText, int skip, int pageSize)
        {
            return await _courseRepository.GetCourses(courseSearchText, skip, pageSize);
        }

        public async Task UpdateCourse(CourseViewModel course)
        {
            var _course = _mapper.Map<Course>(course);

            await _courseRepository.UpdateCourse(_course);
        }

        public async Task<int> GetCourseCount(string searchText)
        {
            var count = await _courseRepository.GetCourseCount(searchText);
            return count;
        }
    }
}
