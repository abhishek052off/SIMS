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

        public async Task DeleteUser(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
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
    }
}
