using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Enrollment> Enrollments { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
