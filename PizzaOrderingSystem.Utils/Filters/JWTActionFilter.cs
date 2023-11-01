using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using PizzaOrderingSystem.DataAccess.Models;
using PizzaOrderingSystem.DTO.IService;
using PizzaOrderingSystem.DTO.Services;
using PizzaOrderingSystem.Utils.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Utils.Filters
{
    public class JWTActionFilter : Attribute, IActionFilter
    {
        private readonly IUserService _userService;
        private readonly IJwtUtils _jwtUtils;
        public string maxRequestPerSecond { get; set; }


        public JWTActionFilter(IUserService userService, IJwtUtils jwtUtils)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string str = maxRequestPerSecond;
            var token = filterContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (_jwtUtils.ValidateToken(token))
            {
                // attach user to context on successful jwt validation
                filterContext.HttpContext.Items["User"] = _userService.GetUsersAsync(); //GetById(userId.Value);
            }
            else
                throw new Exceptions.UnauthorizedAccessException("JWT Token validation fail.");
        }
    }
}
