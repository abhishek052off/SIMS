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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SIMSDBContext _dbContext;

        public TeacherRepository(SIMSDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTeacher(Teacher teacher)
        {
            _dbContext.Teachers.Add(teacher);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTeacher(Teacher teacher)
        {
            _dbContext.Teachers.Remove(teacher);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            var teacher = await _dbContext.Teachers
                .Include(t => t.User)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Teacher not found");
            return teacher;
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            var teachers = await _dbContext.Teachers.ToListAsync();
            return teachers;
        }

        public async Task UpdateTeacher(Teacher teacher)
        {
            _dbContext.Teachers.Update(teacher);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Teacher> GetTeacherByUserId(int id)
        {
            var teacher = await _dbContext.Teachers
                .Include(s => s.User)
                .Where(u => u.UserId == id)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Teacher not found");
            return teacher;
        }
    }
}
