using System;
using System.Threading.Tasks;
using AuthService.Model;
using AuthService.WebApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthService.WebApi.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] AccountDto dto)
        {
            User user = await _userService.Register(dto.Login, dto.Password);
            return Ok(new UserDto(user));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Authenticate([FromBody] AccountDto dto)
        {
            User user = await _userService.Authenticate(dto.Login, dto.Password);

            // SetTokenCookie(user.Token!);
            return Ok(new UserWithTokenDto(user));
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(AuthOptions.LifetimeDays)
            };
            Response.Cookies.Append("token", token, cookieOptions);
        }

        /*-----------------*/

        [HttpGet("noauth")]
        public ActionResult<string> GetNoAuth()
        {
            return Ok("Ok - noauth");
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetAuth()
        {
            return Ok("Ok - auth");
        }

        // Use custom attribute with enum
        [Authorize(Roles = "admin")]
        [HttpGet("admin")]
        public ActionResult<string> GetAdmin()
        {
            return Ok("Ok - admin");
        }

        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
    }
}
