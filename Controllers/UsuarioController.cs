﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Repository;
using WebApi.Models;
using WebApi.DTO;
using WebApi.ViewModels;

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
        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            var viewModel = users.Select(u => new UserViewModel {
                id = u.id,
                name = u.name,
                email = u.email,
                roles = u.roles,
            });
            
            return viewModel;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            
            if (user == null) {
                return NotFound();
            }
            
            var viewModel = new UserViewModel
            {
                id = user.id,
                name = user.name,
                email = user.email,
                roles = user.roles,
            };
            
            return Ok(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> StoreUser([FromBody] UserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = new User
            {
                id = dto.id,
                name = dto.name,
                email = dto.email,
                password = dto.password,
                roles = dto.roles,
            };

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
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = new User
            {
                id = dto.id,
                name = dto.name,
                email = dto.email,
                password = dto.password,
                roles = dto.roles,
            };

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