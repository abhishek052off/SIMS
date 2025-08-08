using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.ViewModels
{
    public class SubmissionViewModel
    {
        public int Id { get; set; }

        public int AssignmentId { get; set; }

        public int StudentId { get; set; }

        public string? StudentName { get; set; } = String.Empty;

        public double Score { get; set; }
        public string? Feedback { get; set; } = String.Empty;
        public DateTime SubmittedAt { get; set; }
    }
}
