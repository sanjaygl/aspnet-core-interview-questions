using API.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var result = await _userService.LoginAsync(request);

            if (!result.Success)
            {
                return Unauthorized(new { Message = result.Message });
            }

            return Ok(new { Message = result.Message, Token = result.AccessToken });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var result = await _userService.RegisterAsync(request);

            if (!result.Success)
            {
                return BadRequest(new { Message = result.Message });
            }

            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenRequest request)
        {
            var result = await _userService.RefreshTokenAsync(request);

            if (!result.Success)
            {
                return Unauthorized(new { Message = result.Message });
            }

            return Ok(new
            {
                Message = result.Message,
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken
            });
        }
    }
}
