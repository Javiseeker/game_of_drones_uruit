using System.Threading.Tasks;
using GameOfDrones.API.Data.Interfaces;
using GameOfDrones.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameOfDrones.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        public UserController(IUserRepository repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {

            user.UName = user.UName.ToLower().Trim();

            if (await _repo.VerifyUserExists(user.UName))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = await _repo.CreateUser(user);

            return Created($"api/User/{userToCreate.UUid}", userToCreate);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var userToUpdate = await _repo.UpdateUser(user);

            if (userToUpdate != null)
            {
                return Ok(userToUpdate);
            }
            else
            {
                return NotFound($"Couldn't perform the update operation successfully on requested item. ({userToUpdate})");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(User user)
        {
            var operation = await _repo.DeleteUser(user);

            if (operation == true)
            {
                return Ok("The item was successfully deleted.");
            }
            else
            {
                return NotFound($"Couldn't delete audit action with the given id. (id:{user.UUid})");
            }
        }

    }
}