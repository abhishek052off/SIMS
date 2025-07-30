using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public Student Student { get; set; }       
        public Course Course { get; set; }

        public int Term { get; set; }
        public double Marks { get; set; }
        public string Comments { get; set; } = string.Empty;
    }
}
