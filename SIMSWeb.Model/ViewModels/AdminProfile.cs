using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.ViewModels
{
    public class ProfileStatus
    {
        public int TotalUsers { get; set; }
        public int ActiveCourses { get; set; }
        public double AvgClassSizePerCourse { get; set; }
    }

    public class RecentEnrollment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }

    public class PendingAssignments
    {
        public string CourseName { get; set; }
        public bool AssignmentStatus { get; set; }
    }

    public class AdminProfile
    {
        public ProfileStatus Status { get; set; }
        public User User { get; set; }
        public List<RecentEnrollment> RecentEnrollments { get; set; }
        public List<PendingAssignments> PendingAssignments { get; set; }
    }
}
