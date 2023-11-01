using PizzaOrderingSystem.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DTO.IService
{
    public interface IAuthenticationService
    {
        Task<string> Register(UserDTO user);
        Task<string> Login(UserDTO user);
    }
}
