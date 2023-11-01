using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PizzaOrderingSystem.DTO.IService;
using PizzaOrderingSystem.DTO.Models;
using PizzaOrderingSystem.DTO.Services;
using PizzaOrderingSystem.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Utils.Jwt
{
    public class JwtUtils : IJwtUtils
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtUtils> _logger;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public JwtUtils(IConfiguration configuration, ILogger<JwtUtils> logger, IUserService userService, IUserRoleService userRoleService)
        {
            _configuration = configuration;
            _logger = logger;
            _userService = userService;
            _userRoleService = userRoleService;
        }

        public string GenerateToken(string userName, string password)
        {
            _logger.LogInformation($"Generating token for user - {userName}");

            UserDTO user = _userService.GetUserByIdPassword(userName, password);
            if (user != null)
            {
                string role = _userRoleService.GetRoleByUserId(user.Id)?.Role;

                // generate token that is valid for 5 mins
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            else
                throw new UserNotFoundException($"UserName - {userName}");
        }

        public bool ValidateToken(string token)
        {
            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userName =  jwtToken.Claims.Where(x => x.Type.Equals("unique_name")).FirstOrDefault().Value;

                // return user id from JWT token if validation successful
                return true;
            }
            catch
            {
                // return null if validation fails
                return false;
            }
        }
    }
}
