using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.ViewModels
{
    public class TeacherProfileMetrics
    {
        public int ActiveCourses { get; set; }
        public int TotalStudentsTaught { get; set; }
        public int TotalAssignmentsCreated { get; set; }

    }

    public class AssignmentList
    {
        public int AssignmentId { get; set; }
        public string AssignmentName { get; set; }
    }

    public class TeacherCourses
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public List<AssignmentList> AssignmentCreated { get; set; }
        public List<string> StudentsEnrolled { get; set; }
    }

    public class RecentlyEnrolledStudents
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }

    public class TeacherProfile
    {
        public TeacherProfileMetrics TeacherProfileMetrics { get; set; }
        public User User { get; set; }
        public List<TeacherCourses> TeacherCourses { get; set; }
        public List<RecentlyEnrolledStudents> RecentlyEnrolledStudents { get; set; }

    }
}
