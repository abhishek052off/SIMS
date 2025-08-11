using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int? TeacherId { get; set; }

        public bool ModifyTeacher { get; set; } = false;
        public string? Department { get; set; } = string.Empty;

        public bool EnrollStudents { get; set; } = false;
        public int? StudentId { get; set; }
        public int? Term { get; set; } = 1;
        public double? Marks { get; set; }
        public string? Comments { get; set; } = string.Empty;
    }
}
