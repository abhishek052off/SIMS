using Microsoft.AspNetCore.Mvc.Rendering;
using SIMSWeb.Business.ServiceDTO.Teacher;
using SIMSWeb.Model.ViewModels;

namespace SIMSWeb.Models.Course
{

    public class AddCourseVM
    {
        public CourseViewModel CourseModel { get; set; }
        public List<TeacherSelect> TeacherListModel { get; set; }
    }
}
