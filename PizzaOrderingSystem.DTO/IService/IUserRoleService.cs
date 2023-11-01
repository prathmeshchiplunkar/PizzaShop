using PizzaOrderingSystem.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DTO.IService
{
    public interface IUserRoleService
    {
        public Task<IEnumerable<UserRoleDTO>> GetUserRolesAsync();
        public Task<bool> InsertAsync(UserRoleDTO userRoleDTO);
        public Task<int> CompletedAsync();
        public UserRoleDTO GetRoleByUserId(int userId);
    }
}
