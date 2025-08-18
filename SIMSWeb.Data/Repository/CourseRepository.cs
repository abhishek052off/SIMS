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
using SIMSWeb.Model.ViewModels;
using SIMSWeb.ConstantsAndUtilities.CourseUtilities;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SIMSWeb.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SIMSDBContext _context;
        private readonly UserSession _userSession;
        private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(SIMSDBContext context, UserSession session, ILogger<CourseRepository> logger)
        {
            _context = context;
            _userSession = session;
            _logger = logger;
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
                  .Include(c => c.Assignments)
                    .ThenInclude(a => a.Submissions)
                    .ThenInclude(s => s.Student);

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
                .ThenInclude(t => t.User);

            if (skip > 0)
            {
                courses = courses.Skip(skip);
            }

            if (pageSize > 0)
            {
                courses = courses.Take(pageSize);
            }


            var courseList = await courses.ToListAsync();
            return courseList;
        }

        public async Task<int> GetActiveCoursesCount()
        {
            var courses = _context.Courses
                .Where(c => c.IsActive);

            return await courses.CountAsync();
        }

        public async Task<double> AverageClassSize()
        {
            var courseClassSizes = await _context.Enrollments
                .GroupBy(e => e.CourseId)
                .Select(g => g.Count())
                .ToListAsync();

            double averageClassSize = courseClassSizes.Any()
                ? courseClassSizes.Average()
                : 0;

            return averageClassSize;
        }

        public async Task<List<Course>> GetCourseDueInSevenDays(string role, int? userId)
        {
            var today = DateTime.Today;
            var next7Days = today.AddDays(7);

            var coursesWithDueAssignments = _context.Courses
                .Include(c => c.Enrollments)
                .Where(course => course.Assignments
                    .Any(a => a.DueDate >= today && a.DueDate <= next7Days));

            if (userId > 0)
            {
                if (role == UsersConstants.TEACHER_ROLE)
                {
                    coursesWithDueAssignments = coursesWithDueAssignments.Where(c =>
                    c.TeacherId == userId);
                }
                else if (role == UsersConstants.STUDENT_ROLE)
                {
                    coursesWithDueAssignments = coursesWithDueAssignments.Where(c =>
                    c.Enrollments.Any(e => e.StudentId == userId));
                }

            }

            return await coursesWithDueAssignments.ToListAsync();
        }

        public async Task<List<AssignmentProgress>> GetProgressofStudent(int userId)
        {
            // Step 1: Retrieve courses with assignments and submissions for a specific student
            var courses = await _context.Courses
                .Include(course => course.Assignments)
                    .ThenInclude(assignment => assignment.Submissions)
                .Where(course => course.Assignments
                    .Any(assignment => assignment.Submissions
                        .Any(submission => submission.Student.UserId == userId))) // Filter by student
                .ToListAsync();

            // Step 2: Perform in-memory LINQ operations (GroupBy, Max, etc.)
            var assignmentProgresses = courses
                .SelectMany(course => course.Assignments)
                .SelectMany(assignment => assignment.Submissions)
                .Where(submission => submission.Student.UserId == userId)
                .GroupBy(submission => submission.AssignmentId)
                .Select(group => new AssignmentProgress
                {
                    AssignmentTitle = group.First().Assignment.Title,
                    Score = group.Max(submission => submission.Score),
                    MaxScore = group.First().Assignment.MaxScore,
                    ProgressColor = CourseUtlity.CalculateProgressColor(group.Max(sub => sub.Score), group.First().Assignment.MaxScore)
                })
                .ToList();

            _logger.LogInformation($"==Assignment progress of each student { JsonConvert.SerializeObject(assignmentProgresses, Formatting.Indented)} ");

            return assignmentProgresses;
        }
    }
}
