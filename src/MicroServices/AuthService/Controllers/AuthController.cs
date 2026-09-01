using Microsoft.AspNetCore.Mvc;
using AuthService.Services.Users;

namespace AuthService.Controllers
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
        public async Task<IActionResult> LoginAsync(Models.LoginRequest request)
        {
            var result = await _userService.LoginAsync(request);

            if (!result.Success)
            {
                return Unauthorized(new { Message = result.Message });
            }

            return Ok(new { Message = result.Message });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(Models.RegisterRequest request)
        {
            var result = await _userService.RegisterAsync(request);

            if (!result.Success)
            {
                return BadRequest(new { Message = result.Message });
            }

            return CreatedAtAction(null, result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var result = await _userService.RefreshTokenAsync();

            if (!result.Success)
            {
                return Unauthorized(new { Message = result.Message });
            }

            return Ok(new
            {
                Message = result.Message,
            });
        }
    }
}
