using SIMSWeb.Business.ServiceDTO.StudentDTO;
using SIMSWeb.Business.ServiceDTO.Teacher;
using SIMSWeb.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.ServiceDTO.CourseDTO
{

    public class UpdateCourseVM
    {
        public CourseViewModel Course { get; set; }
        public List<TeacherSelect>? TeachersList { get; set; }
        public List<StudentSelect>? StudentList { get; set; }

    }
}
