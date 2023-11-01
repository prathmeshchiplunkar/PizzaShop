using PizzaOrderingSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DataAccess.IRepositories
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        public UserRole GetRoleByUserId(int userId);
    }
}
