using SIMSWeb.Models.User;

namespace SIMSWeb.Models.Course
{
    public class ManageCourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class ManageCourseVM
    {
        public List<ManageCourseModel> Courses { get; set; }
        public PaginatedResult<ManageCourseModel> Paginations { get; set; }

    }
}
