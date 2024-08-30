using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
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