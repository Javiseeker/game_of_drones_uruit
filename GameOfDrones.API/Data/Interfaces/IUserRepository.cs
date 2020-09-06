using System.Collections.Generic;
using System.Threading.Tasks;
using GameOfDrones.API.Models;

namespace GameOfDrones.API.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(User user);

        Task<bool> VerifyUserExists(string username);

        Task<int> GetUserIdByName(string name);
    }
}