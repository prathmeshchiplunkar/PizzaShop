using PizzaOrderingSystem.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DTO.IService
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetUsersAsync();
        public Task<bool> InsertAsync(UserDTO userDTO);
        public Task<int> CompletedAsync();
        public UserDTO GetUserByIdPassword(string userName, string password);

        public UserDTO CheckUserAvailibility(string userName, string email);

        
    }
}
