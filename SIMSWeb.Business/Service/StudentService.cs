using AutoMapper;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.ServiceDTO.StudentDTO;
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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepo, IMapper mapper)
        {
            _studentRepository = studentRepo;
            _mapper = mapper;
        }

        public async Task AddStudent(StudentViewModel student)
        {
            var _student = _mapper.Map<Student>(student);
            await _studentRepository.AddStudent(_student);
        }

        public async Task DeleteStudent(Student student)
        {
            await _studentRepository.DeleteStudent(student);
        }

        public async Task<List<StudentSelect>> GetStudents(int courseId)
        {
            var students = await _studentRepository.GetStudents(courseId);
            return GetStudentSelectList(students);
        }

        public async Task<List<StudentSelect>> GetEnrolledStudentsByCourseId(int courseId)
        {
            var students = await _studentRepository.GetEnrolledStudentsByCourseId(courseId);
            return GetStudentSelectList(students);
        }

        public async Task<List<Enrollment>> GetRecentEnrolledStudents()
        {
            var enrollments = await _studentRepository.GetRecentEnrolledStudents();
            return enrollments;
        }

        public List<StudentSelect> GetStudentSelectList(List<Student> students)
        {
            var studentList = students.Select(u => new StudentSelect
            {
                Id = u.Id,
                Name = u.User.Name,
            }).ToList();

            studentList.Insert(0, new StudentSelect
            {
                Id = -1,
                Name = "Select Student"
            });

            return studentList;
        }

        public async Task UpdateStudent(Student student)
        {
            await _studentRepository.UpdateStudent(student);
        }

        public async Task EnrollStudents(EnrollmentViewModel enrolledDetails)
        {
            var enrolledData = _mapper.Map<Enrollment>(enrolledDetails);
            await _studentRepository.EnrollStudents(enrolledData);
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _studentRepository.GetStudentById(id);
        }

        public async Task<List<Enrollment>> GetStudentEnrolledByUserId(int teacherId)
        {
            return await _studentRepository.GetStudentEnrolledByUserId(teacherId);
        }
    }
}
