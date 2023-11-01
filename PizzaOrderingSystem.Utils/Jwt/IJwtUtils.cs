using PizzaOrderingSystem.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Utils.Jwt
{
    public interface IJwtUtils
    {
        public string GenerateToken(string userName, string password);
        public bool ValidateToken(string token);
    }
}
