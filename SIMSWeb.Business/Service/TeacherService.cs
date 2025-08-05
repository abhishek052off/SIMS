using SIMSWeb.Business.IService;
using SIMSWeb.ConstantsAndUtilities.AuthUtilities;
using SIMSWeb.Data.Exceptions;
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

        public async Task UpdateTeacher(TeacherViewModel teacher)
        {
            var teacherData = await _teacherRepository.GetTeacherById(teacher.Id);
            if (teacherData == null)
            {
                throw new NotFoundException("Teacher not found.");
            }

            if (teacherData.Department != teacher.Department)
            {
                teacherData.Department = teacher.Department;
            }

            if (teacher.HireDate != teacher.HireDate)
            {
                teacherData.HireDate = teacher.HireDate;
            }

            if (teacher.UserId != teacher.UserId)
            {
                teacherData.UserId = teacher.UserId;
            }

            await _teacherRepository.UpdateTeacher(teacherData);
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            return await _teacherRepository.GetTeacherById(id);
        }
    }
}
