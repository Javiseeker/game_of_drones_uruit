using System.Collections.Generic;
using System.Threading.Tasks;
using GameOfDrones.API.Data.Interfaces;
using GameOfDrones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOfDrones.API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context.User.ToListAsync();
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.User.FirstOrDefaultAsync(us=> us.UUid == id);
            return user;
        }

        public async Task<User> CreateUser(User user)
        {
            var result = await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            var userInDB = await _context.User.FirstOrDefaultAsync(x => x.UUid == user.UUid);

            if (userInDB != null)
            {
                userInDB.UName = user.UName;

                _context.User.Update(userInDB);
                await _context.SaveChangesAsync();
            }

            return userInDB;
        }
        public async Task<bool> DeleteUser(User user)
        {
            var deletionSuccess = false;

            var userInDB = await _context.User.FirstOrDefaultAsync(x => x.UUid == user.UUid);

            if (userInDB != null)
            {
                _context.User.Remove(userInDB);
                await _context.SaveChangesAsync();
                deletionSuccess = true;
            }

            return deletionSuccess;
        }

        public async Task<bool> VerifyUserExists(string username)
        {
            if (await _context.User.AnyAsync(x=> x.UName == username)) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public async Task<int> GetUserIdByName(string name)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.UName == name);
            return user.UUid;
        }
    }
}