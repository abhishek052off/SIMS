using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; } = String.Empty;
        public int MaxScore { get; set; }
        public DateTime DueDate { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public List<Submission> Submissions { get; set; }

    }
}
