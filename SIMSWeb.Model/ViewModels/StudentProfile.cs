using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.ViewModels
{
    public class StudentProfileMetrics
    {
        public int ActiveCourses { get; set; }
        public int ActiveAssignments { get; set; }
        public int CompletedAssignmentsCount { get; set; }
        public int MaximumMarks { get; set; }

    }

    public class StudentProfile
    {
        public StudentProfileMetrics StudentProfileMetrics { get; set; }
        public User User { get; set; }
    }
}
