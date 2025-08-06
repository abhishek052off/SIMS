using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.ViewModels
{
    public class EnrollmentViewModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Term { get; set; }
        public double Marks { get; set; }
        public string Comments { get; set; } = string.Empty;
    }
}
