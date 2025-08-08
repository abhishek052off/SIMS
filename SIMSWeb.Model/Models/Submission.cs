using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.Models
{
    public class Submission
    {
        public int Id { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public double Score { get; set; }
        public string? Feedback { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }
    }
}
