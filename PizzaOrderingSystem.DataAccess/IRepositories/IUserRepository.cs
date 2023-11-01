using PizzaOrderingSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DataAccess.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetUserByIdPassword(string userName , string password);
        public User CheckUserAvailibility(string userName, string email);
    }
}
