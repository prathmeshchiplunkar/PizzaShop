using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog.Fluent;
using PizzaOrderingSystem.DTO.IService;
using PizzaOrderingSystem.DTO.Models;
using PizzaOrderingSystem.DTO.Services;
using PizzaOrderingSystem.Utils.Exceptions;
using PizzaOrderingSystem.Utils.Jwt;

namespace PizzaOrderingSystem.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserService _service;
        private readonly IJwtUtils _jwtUtils;
        public SecurityController(ILogger<WeatherForecastController> logger, IUserService service, IJwtUtils jwtUtils)
        {
            _logger = logger;
            _service = service;
            _jwtUtils = jwtUtils;
        }

        
        [HttpPost("Login")]
        public IActionResult Login(string userName, string password)
        {
            string tokenString = _jwtUtils.GenerateToken(userName, password);
            if (string.IsNullOrEmpty(tokenString))
                throw new UserNotFoundException($"Cannot generate token for UserName - {userName}");
            return Ok(new { token = tokenString });

        }

        [HttpPost("GenerateToken")]
        public async Task<string> RegisterUser([FromBody] UserDTO user)
        {
            UserDTO userDTO = _service.CheckUserAvailibility(user.UserName, user.Email);
            if (userDTO != null)
                throw new UserAlreadyRegisterdException($"{user.UserName}");
            bool isSuccess = await _service.InsertAsync(user);
            if (!isSuccess)
                throw new UserNotFoundException($"Cannot create user - {user.UserName}");
            return "User created Successfully";

        }



    }
}
