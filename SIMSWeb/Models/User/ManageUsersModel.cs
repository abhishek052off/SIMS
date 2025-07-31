using Microsoft.AspNetCore.Mvc.Rendering;
using SIMSWeb.Model.Models;

namespace SIMSWeb.Models.User
{
    public class ManageUsersModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class FilterModel
    {
        public SelectList Roles { get; set; }
    }

    public class ManageUserVM
    {
        public List<ManageUsersModel> Users { get; set; }
        public FilterModel Filters { get; set; }
        public PaginatedResult<ManageUsersModel> Paginations { get; set; }

    }

}
