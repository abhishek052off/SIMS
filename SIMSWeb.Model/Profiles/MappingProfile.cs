using AutoMapper;
using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();

            CreateMap<Course, CourseViewModel>();
            CreateMap<CourseViewModel, Course>();

            CreateMap<Teacher, TeacherViewModel>();
            CreateMap<TeacherViewModel, Teacher>();

            CreateMap<Student, StudentViewModel>();
            CreateMap<StudentViewModel, Student>();

            CreateMap<Enrollment, EnrollmentViewModel>();
            CreateMap<EnrollmentViewModel, Enrollment>();

            CreateMap<Assignment, AssignmentViewModel>();
            CreateMap<AssignmentViewModel, Assignment>();
        }
        
    }
}
