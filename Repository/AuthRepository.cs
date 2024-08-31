using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;
using WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AuthRepository(ApplicationDbContext dBContext)
    {
        _dbContext = dBContext;
    }

    public async Task<User?> ValidateUserAsync(string email, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.email == email);

        if (user == null || !VerifyPassword(password, user.password))
        {
            return null;
        }

        return user;
    }

    public string CreateToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();
  
        var privateKey = Encoding.UTF8.GetBytes(Configuration.PrivateKey);
          
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(privateKey),
            SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "yourIssuer",
            Audience = "yourAudience",
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = GenerateClaims(user)
        };
        
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();
  
        ci.AddClaim(new Claim("id", user.id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.name));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, user.name));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.email));
  
        foreach (var role in user.roles)
        {
            ci.AddClaim(new Claim(ClaimTypes.Role, role));
        }
        
        return ci;
    }

    private bool VerifyPassword(string enteredPassword, string storedPassword)
    {
        return enteredPassword == storedPassword;
    }
}