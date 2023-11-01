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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PizzaDbContext context, ILogger logger) : base(context, logger)
        {
             
        }

        public User CheckUserAvailibility(string userName, string email)
        {
            return _context.Users.Where(u => u.UserName.Equals(userName, StringComparison.Ordinal) && u.Email.Equals(email, StringComparison.Ordinal)).FirstOrDefault();
        }

        public User GetUserByIdPassword(string userName, string password)
        {
            return _context.Users.Where(u => u.UserName.Equals(userName, StringComparison.Ordinal) && u.Password.Equals(password, StringComparison.Ordinal)).FirstOrDefault();
        }
    }
}