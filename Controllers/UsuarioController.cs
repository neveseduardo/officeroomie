using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Repository;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            
            if (user == null) {
                return NotFound();
            }
            
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> StoreUser([FromBody] User user)
        {
            user.password = PasswordHelper.HashPassword(user.password);
            await _userRepository.AddUserAsync(user);
            return CreatedAtAction("GetUser", new { id = user.id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var user = await _userRepository.DeleteUserAsync(id);
            
            if (user == null) {
                return NotFound();
            }
            
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] User user)
        {
            if (id != user.id) {
                return BadRequest();
            }

            var updatedUser = await _userRepository.UpdateUserAsync(id, user);
            
            if (updatedUser == null) {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatchUser([FromRoute] int id, [FromBody] JsonPatchDocument userDocument)
        {
            var updatedUser = await _userRepository.UpdateUserPatchAsync(id, userDocument);
            
            if (updatedUser == null) {
                return NotFound();
            }
            
            return Ok(updatedUser);
        }
    }
}