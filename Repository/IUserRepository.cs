﻿using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;

namespace WebApi.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User?> GetUserByIdAsync(int id);

        Task<User?> AddUserAsync(User User);

        Task<User?> DeleteUserAsync(int id);

        Task<User?> UpdateUserAsync(int id, User user);

        Task<User?> UpdateUserPatchAsync(int id, JsonPatchDocument User);
    }
}