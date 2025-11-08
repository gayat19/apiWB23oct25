using FirstAPI.Interfaces;
using FirstAPI.Models.DTOs;
using FirstAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService,ILogger<UserController> logger) 
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CustomerRegisterRequest customer)
        {
            try
            {
                var result = await _userService.Register(customer);
                if(result)
                {
                    return Ok("User registered successfully.");
                }
                else
                {
                    return BadRequest("User registration failed.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            try
            {
                var response = await _userService.ValidateCredentials(user);
                if(response != null)
                {
                    return Ok(response);
                }
                else
                {
                    _logger.LogWarning("Invalid login attempt for user: {Username}", user.Username);
                    return Unauthorized("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Invalid login attempt for user: {Username}", user.Username);
                return Unauthorized("Invalid username or password.");
            }
        }
    }
}
