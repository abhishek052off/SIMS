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
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using SIMSWeb.ConstantsAndUtilities;

namespace SIMSWeb.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SIMSDBContext _context;
        private readonly UserSession _userSession;

        public CourseRepository(SIMSDBContext context, UserSession session)
        {
            _context = context;
            _userSession = session;
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

        public async Task<List<Course>> GetCourses(int teacherFilter,
            string courseSearchText, int skip, int pageSize)
        {
            IQueryable<Course> courses = _context.Courses;

            if (teacherFilter > 0 && teacherFilter != null)
            {
                courses = courses.Include(c => c.Teacher)
                    .Where(t => t.TeacherId == teacherFilter);
            }

            if (!string.IsNullOrEmpty(courseSearchText))
            {
                courses = courses.Where(u => u.Name.Contains(courseSearchText)
                    || (u.Description != null && u.Description.Contains(courseSearchText)));

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

        public async Task<int> GetCourseCount(int teacherFilter, string searchText)
        {
            IQueryable<Course> courses = _context.Courses
                .Include(c => c.Teacher)
                    .ThenInclude(t => t.User);

            if (_userSession.Role == UsersConstants.STUDENT_ROLE)
            {
                courses = courses.Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                    .ThenInclude(s => s.User);
            }

            if ((_userSession.Role == UsersConstants.STUDENT_ROLE ||
                _userSession.Role == UsersConstants.STUDENT_ROLE) && _userSession.Id > 0)
            {
                courses = courses.Where(c => c.Teacher.UserId == _userSession.Id ||
                c.Enrollments.Any(e => e.Student.UserId == _userSession.Id));
            }

            if (teacherFilter > 0 && teacherFilter != null)
            {
                courses = courses.Include(c => c.Teacher)
                    .Where(t => t.TeacherId == teacherFilter);
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                courses = courses.Where(u => u.Name.Contains(searchText));

            }

            return await courses
                .Include(c => c.Teacher).CountAsync();
        }

        public async Task<Course> GetCourseDetailsById(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                    .ThenInclude(t => t.User)
                .Include(c => c.Assignments)
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                    .ThenInclude(s => s.User)                
                 .Where(c => c.Id == id)
                 .FirstOrDefaultAsync();

            return course;
        }

        public async Task<List<Course>> GetCoursesByUserId(int userId, int teacherFilter,
            string courseSearchText, int skip, int pageSize)
        {
            IQueryable<Course> courses = _context.Courses
                .Include(c => c.Teacher)
                    .ThenInclude(t => t.User)
                 .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                    .ThenInclude(s => s.User)
                  .Include(c => c.Assignments);

            if (userId > 0)
            {
                courses = courses.Where(c => c.Teacher.UserId == userId ||
                c.Enrollments.Any(e => e.Student.UserId == userId));
            }

            if (teacherFilter > 0 && teacherFilter != null)
            {
                courses = courses.Include(c => c.Teacher)
                    .Where(t => t.TeacherId == teacherFilter);
            }

            if (!string.IsNullOrEmpty(courseSearchText))
            {
                courses = courses.Where(u => u.Name.Contains(courseSearchText)
                    || (u.Description != null && u.Description.Contains(courseSearchText)));

            }

            courses = courses.Include(c => c.Teacher)
                .ThenInclude(t => t.User)
                .Skip(skip).Take(pageSize);

            var courseList = await courses.ToListAsync();
            return courseList;
        }
    }
}
