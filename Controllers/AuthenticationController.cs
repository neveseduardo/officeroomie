using Microsoft.AspNetCore.Mvc;
using WebApi.Repository;
using WebApi.DTO;

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
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginDto dto)
        {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                if (dto == null || string.IsNullOrEmpty(dto.email) || string.IsNullOrEmpty(dto.password)) {
                    return BadRequest("Invalid client request");
                }

                var user = await _authRepository.ValidateUserAsync(dto.email, dto.password);
                
                if (user == null) {
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