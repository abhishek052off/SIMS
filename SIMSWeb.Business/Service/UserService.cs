using Microsoft.EntityFrameworkCore;
using SIMSWeb.Business.IService;
using SIMSWeb.Data.Exceptions;
using SIMSWeb.Data.IRepository;
using SIMSWeb.Model.Models;
using SIMSWeb.ConstantsAndUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        public UserService(IUserRepository userRepository, IStudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task AddUser(User user)
        {
            await _userRepository.AddUser(user);
            if (user.Role == UsersConstants.STUDENT_ROLE)
            {
                var student = new Student
                {
                    UserId = user.Id,
                    EnrollmentDate = DateTime.Now,
                };

                await _studentRepository.AddStudent(student);
            }
            else if (user.Role == UsersConstants.TEACHER_ROLE)
            {
                var teacher = new Teacher
                {
                    UserId = user.Id,
                    HireDate = DateTime.Now,
                };

                await _teacherRepository.AddTeacher(teacher);
            }
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            User user = await _userRepository.GetUserByEmail(email) ?? throw new NotFoundException("User not found with this email");
            if (user.Password != password)
            {
                throw new CustomException("Password is incorrect");

            }

            return user;
        }

        public async Task DeleteUser(User user)
        {
            await _userRepository.DeleteUser(user);
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return users;
        }

        public async Task UpdateUser(int? id)
        {
            if (String.IsNullOrEmpty(id.ToString()) || id == 0)
            {
                throw new NotFoundException("Not a valid Id");
            }

            if (id.HasValue)
            {
                User? user = await _userRepository.GetUserById(id.Value) ?? throw new NotFoundException("User not found");
                await _userRepository.UpdateUser(user);
            }

        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
    }
}
