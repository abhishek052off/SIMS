using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public DateTime HireDate { get; set; }
        public string? Department { get; set; }

        public List<Course> Courses { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
