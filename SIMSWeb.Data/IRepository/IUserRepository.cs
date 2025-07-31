using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Data.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers(string userRole, string searchText, int skip, int pageSize);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task<int> GetUserCount(string userRole, string searchText);
    }
}
