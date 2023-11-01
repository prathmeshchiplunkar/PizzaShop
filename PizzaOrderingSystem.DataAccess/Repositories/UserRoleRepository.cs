using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.DataAccess.Data;
using PizzaOrderingSystem.DataAccess.IRepositories;
using PizzaOrderingSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DataAccess.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(PizzaDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public UserRole GetRoleByUserId(int userId)
        {
            return _context.UserRoles.Where(u => u.UserId.Equals(userId)).FirstOrDefault();
        }

    }
}