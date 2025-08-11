using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSWeb.Model.Models;

namespace SIMSWeb.Business.ServiceDTO.SubmissionDTO
{
    public class SubmissionServiceModel
    {
        public int Id { get; set; }

        public int AssignmentId { get; set; }

        public int StudentId { get; set; }

        public double Score { get; set; }
        public string? Feedback { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }
    }
}
