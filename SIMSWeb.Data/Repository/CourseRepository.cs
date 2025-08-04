using Microsoft.EntityFrameworkCore;
using SIMSWeb.Data.Context;
using SIMSWeb.Data.Exceptions;
using SIMSWeb.Data.IRepository;
using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SIMSDBContext _context;

        public CourseRepository(SIMSDBContext context)
        {
            _context = context;
        }

        public async Task AddCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourse(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<Course> GetCourseById(int id)
        {
           var course = await _context.Courses
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Course not found");
            return course;
        }

        public async Task<List<Course>> GetCourses(string courseSearchText, int skip, int pageSize)
        {
            IQueryable<Course> courses = _context.Courses;

            if (!string.IsNullOrEmpty(courseSearchText))
            {
                courses = courses.Where(u => u.Name.Contains(courseSearchText));

            }

            courses = courses.Include(c => c.Teacher)
                .ThenInclude(t => t.User)
                .Skip(skip).Take(pageSize);

            var courseList = await courses.ToListAsync();
            return courseList;
        }

        public async Task UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCourseCount( string searchText)
        {
            IQueryable<Course> courses = _context.Courses;

            if (!string.IsNullOrEmpty(searchText))
            {
                courses = courses.Where(u => u.Name.Contains(searchText));

            }

            return await courses.CountAsync();
        }

        public async Task<Course> GetCourseDetailsById(int id)
        {
            var course = await _context.Courses
                 .Where(c => c.Id == id)
                 .FirstOrDefaultAsync() ?? throw new NotFoundException("Course not found");
            return course;
        }
    }
}
