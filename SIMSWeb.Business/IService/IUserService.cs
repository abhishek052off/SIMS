using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Business.IService
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
        Task<User> AuthenticateUser(string email, string password);
    }
}
