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
        public bool IsActive { get; set; }
        public int? TeacherId { get; set; }

        public bool ModifyTeacher { get; set; } = false;
        public string? Department { get; set; } = String.Empty;
    }
}
