using Microsoft.AspNetCore.Mvc.Rendering;
using SIMSWeb.Models.Teacher;

namespace SIMSWeb.Models.Course
{
    public class CourseViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public int? TeacherId { get; set; }
    }


    public class AddCourseVM
    {
        public CourseViewModel CourseModel { get; set; }
        public List<TeacherSelect> TeacherListModel { get; set; }
    }
}
