using SIMSWeb.Business.ServiceDTO.Student;
using SIMSWeb.Business.ServiceDTO.Teacher;
using SIMSWeb.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.ServiceDTO.Submission
{
    public class UpdateSubmission
    {
        public SubmissionViewModel Submission { get; set; }
        public List<StudentSelect>? StudentList { get; set; }
    }
}
