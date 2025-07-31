using Microsoft.EntityFrameworkCore;
using SIMSWeb.Data.Context;
using SIMSWeb.Data.Exceptions;
using SIMSWeb.Data.IRepository;
using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SIMSDBContext _dbContext;

        public UserRepository(SIMSDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task AddUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await GetUserById(id);
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsers(string userRole, string searchText, int skip, int pageSize)
        {
            IQueryable<User> users = _dbContext.Users;

            if (!string.IsNullOrEmpty(userRole))
            {
                users = users.Where(x => x.Role == userRole);
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                users = users.Where(u => u.Name.Contains(searchText)
                || u.Email.Contains(searchText));

            }

            users = users.Skip(skip).Take(pageSize);
            return await users.ToListAsync();
        }

        public async Task UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("User not found");
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _dbContext.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("User not found");
            return user;

        }

        public async Task<int> GetUserCount(string userRole, string searchText)
        {
            IQueryable<User> users = _dbContext.Users;

            if (!string.IsNullOrEmpty(userRole))
            {
                users = users.Where(x => x.Role == userRole);
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                users = users.Where(u => u.Name.Contains(searchText)
                || u.Email.Contains(searchText));

            }

            return await users.CountAsync();
        }
    }
}
