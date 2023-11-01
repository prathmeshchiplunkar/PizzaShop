using Microsoft.AspNetCore.Http;
using PizzaOrderingSystem.DTO.IService;
using PizzaOrderingSystem.Utils.Exceptions;
using PizzaOrderingSystem.Utils.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Utils.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetUsersAsync(); //GetById(userId.Value);
            }
            else
                throw new Exceptions.UnauthorizedAccessException("JWT Token validation fail.");

            await _next(context);
        }
    }
}
