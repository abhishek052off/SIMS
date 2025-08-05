using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Model.ViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        public DateTime HireDate { get; set; }
        public string? Department { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}
