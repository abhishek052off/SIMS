using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.Models
{
    public class Student
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public List<Enrollment> Enrollments { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
