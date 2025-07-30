using SIMSWeb.Business.IService;
using SIMSWeb.Data.IRepository;
using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepo)
        {
            _studentRepository = studentRepo;
        }

        public async Task AddStudent(Student student)
        {
            await _studentRepository.AddStudent(student);
        }

        public async Task DeleteStudent(Student student)
        {
            await _studentRepository.DeleteStudent(student);
        }

        public async Task<List<Student>> GetStudents()
        {
            return await _studentRepository.GetStudents();
        }

        public async Task UpdateStudent(Student student)
        {
            await _studentRepository.UpdateStudent(student);
        }
    }
}
