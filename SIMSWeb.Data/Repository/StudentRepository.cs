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
    public class StudentRepository : IStudentRepository
    {
        private readonly SIMSDBContext _dbContext;
        public StudentRepository(SIMSDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddStudent(Student student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteStudent(Student student)
        {
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await _dbContext.Students
                .Include(s => s.User)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Student not found");
            return student;
        }

        public async Task<List<Student>> GetStudents()
        {
            var students = await _dbContext.Students.ToListAsync();
            return students;
        }

        public async Task UpdateStudent(Student student)
        {
            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Student> GetStudentByUserId(int id)
        {
            var student = await _dbContext.Students
                .Include(s => s.User)
                .Where(u => u.UserId == id)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Student not found");
            return student;
        }
    }
}
