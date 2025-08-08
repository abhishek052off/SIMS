using System.Security.Cryptography.X509Certificates;

namespace SIMSWeb.Models.Course
{
    public class StudentViewDTO
    {
        public string StudentName { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public int Term { get; set; }
        public double Marks { get; set; }
        public string Comments { get; set; } = string.Empty;

        public int? UserId { get; set; }
    }

    public class ViewCourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int? TeacherId { get; set; }
        public string? TeacherName { get; set; }

        public DateTime? TeacherHireDate { get; set; }

        public string? Department { get; set; }

        public List<StudentViewDTO>? Students { get; set;}

        public int? AssignmentsCount { get; set; }
    }
}
