using PizzaOrderingSystem.DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DataAccess.Configuration
{
    public interface IUnitOfWork : IDisposable
    {
        IPizzaInventoryRepository PizzaInventoryRepository { get; }
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        Task<int> CompletedAsync();
    }
}
