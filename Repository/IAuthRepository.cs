using WebApi.Models;

namespace WebApi.Repository
{
    public interface IAuthRepository
    {
        Task<User?> ValidateUserAsync(string email, string password);
        string CreateToken(User user);
    }
}