using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.ViewModels
{
    public class AssignmentViewModel
    {
        public string Title { get; set; }
        public string? Description { get; set; } = string.Empty;
        public int MaxScore { get; set; }
        public DateTime DueDate { get; set; }

        public string? CourseName { get; set; } = string.Empty;
        public int CourseId { get; set; }

        public List<Submission>? Submissions { get; set; }
    }
}
