using SIMSWeb.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.ServiceDTO.Course
{
    public class UpdateCourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? TeacherId { get; set; }       
    }

    public class UpdateCourseVM
    {
        public UpdateCourseDTO Course { get; set; }
        public List<TeacherSelect> TeachersList { get; set; }

    }
}
