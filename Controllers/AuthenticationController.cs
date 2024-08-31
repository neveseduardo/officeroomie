using Microsoft.AspNetCore.Mvc;
using WebApi.Repository;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/v1/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthenticationController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginViewModel loginRequest)
        {
            try
            {
                if (loginRequest == null || string.IsNullOrEmpty(loginRequest.email) || string.IsNullOrEmpty(loginRequest.password))
                {
                    return BadRequest("Invalid client request");
                }

                var user = await _authRepository.ValidateUserAsync(loginRequest.email, loginRequest.password);
                
                if (user == null)
                {
                    return Unauthorized();
                }

                var token = _authRepository.CreateToken(user);
                
                return Ok(new { Token = token });
            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}