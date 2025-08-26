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
using SIMSWeb.ConstantsAndUtilities.AuthUtilities;
using SIMSWeb.Model.ViewModels;
using SIMSWeb.Business.ServiceDTO.User;

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
            user.Password = PasswordUtility.HashPassword(user.Password);

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
            var isHashMatch = PasswordUtility.VerifyPassword(password, user.Password);

            if (!isHashMatch)
            {
                throw new CustomException("Password is incorrect");

            }

            return user;
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        public async Task<List<User>> GetUsers(string userRole, string searchText, int skip, int pageSize)
        {
            var users = await _userRepository.GetUsers(userRole, searchText, skip, pageSize);
            return users;
        }

        public async Task<UpdateResponseDTO> UpdateUser(UserViewModel userRequest)
        {
            var user = await _userRepository.GetUserById(userRequest.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            if (user.Name != userRequest.Name)
            {
                user.Name = userRequest.Name;
            }

            if (user.Email != userRequest.Email)
            {
                user.Email = userRequest.Email;
            }

            if (!string.IsNullOrEmpty(userRequest.Password))
            {
                var isPasswordSame = PasswordUtility.VerifyPassword(userRequest.Password, user.Password);
                if (!isPasswordSame)
                {
                    user.Password = PasswordUtility.HashPassword(userRequest.Password);
                }
            }

            if (user.Role != userRequest.Role)
            {
                user.Role = userRequest.Role;

                // Existing Role = Teacher now changed to Student
                if (userRequest.Role == UsersConstants.STUDENT_ROLE)
                {
                    if (user.Teacher.Courses.Count > 0)
                    {
                        return new UpdateResponseDTO
                        {
                            Success = false,
                            Message = "Role update is not allowed for teacher who are assigned course."
                        };
                    }
                    else
                    {
                        //Add to Student table
                        var student = new Student
                        {
                            UserId = user.Id,
                            EnrollmentDate = DateTime.Now,
                        };

                        await _studentRepository.AddStudent(student);

                        //Remove from Teacher table
                        var teacher = await _teacherRepository.GetTeacherByUserId(user.Id);

                        await _teacherRepository.DeleteTeacher(teacher);
                    }

                }
                else if (user.Role == UsersConstants.TEACHER_ROLE)
                {
                    if (user.Student.Enrollments.Count > 0)
                    {
                        return new UpdateResponseDTO
                        {
                            Success = false,
                            Message = "Role update is not allowed for students who are currently enrolled."
                        };
                    }
                    else
                    {
                        var teacher = new Teacher
                        {
                            UserId = user.Id,
                            HireDate = DateTime.Now,
                        };

                        await _teacherRepository.AddTeacher(teacher);

                        //Remove from Student table
                        var student = await _studentRepository.GetStudentByUserId(user.Id);

                        await _studentRepository.DeleteStudent(student);
                    }
                }
            }

            await _userRepository.UpdateUser(user);
            return new UpdateResponseDTO
            {
                Success = true,
                Message = "User updated successfully."
            };

        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<int> GetUserCount(string userRole, string searchText)
        {
            return await _userRepository.GetUserCount(userRole, searchText);
        }
    }
}
