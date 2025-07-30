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
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task AddTeacher(Teacher teacher)
        {
            await _teacherRepository.AddTeacher(teacher);
        }

        public async Task DeleteTeacher(Teacher teacher)
        {
            await _teacherRepository.DeleteTeacher(teacher);
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            return await _teacherRepository.GetTeachers();
        }

        public async Task UpdateTeacher(Teacher teacher)
        {
            await _teacherRepository.UpdateTeacher(teacher);
        }
    }
}
