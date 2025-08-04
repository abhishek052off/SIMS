using SIMSWeb.Model.Models;

namespace SIMSWeb.Models.Course
{
    public class ManageCourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public Teacher Teacher { get; set; }
    }

    public class ManageCourseVM
    {
        public List<ManageCourseModel> Courses { get; set; }
        public PaginatedResult<ManageCourseModel> Paginations { get; set; }

    }
}
