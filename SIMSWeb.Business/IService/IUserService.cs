using SIMSWeb.Model.Models;
using SIMSWeb.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.IService
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(string userRole, string searchText, int skip, int pageSize);
        Task AddUser(User user);
        Task UpdateUser(UserViewModel user);
        Task DeleteUser(int id);
        Task<User> AuthenticateUser(string email, string password);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<int> GetUserCount(string userRole, string searchText);
    }
}
