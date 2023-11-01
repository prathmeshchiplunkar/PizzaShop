using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.DataAccess.Data;
using PizzaOrderingSystem.DataAccess.IRepositories;
using PizzaOrderingSystem.DataAccess.Models;
using PizzaOrderingSystem.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DataAccess.Configuration
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PizzaDbContext _context;
        private readonly ILogger _logger;
        public IPizzaInventoryRepository PizzaInventoryRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IUserRoleRepository UserRoleRepository { get; private set; }
        public UnitOfWork(PizzaDbContext context, ILoggerFactory logger)
        {
            _context = context;
            _logger = logger.CreateLogger("logs");
            PizzaInventoryRepository = new PizzaInventoryRepository(_context, _logger);
            UserRepository = new UserRepository(_context, _logger);
            UserRoleRepository = new UserRoleRepository(_context, _logger);
            LoadSampleDataSet();
        }
        public async Task<int> CompletedAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private void LoadSampleDataSet()
        {
            List<PizzaInventory> pizzaInventory = new List<PizzaInventory>() { 
            new PizzaInventory() { Id = 1, Name = "Smoked Chicken Gourmet Pizza\r\n", Type = 1, Section = 1, Reguler = 599, Medium = 699, Large = 999, Category = 2, Description = "Hot & spicy pizza with onion\r\n" }
            ,new PizzaInventory() { Id = 2, Name = "Margherita", Type = 2, Section = 2, Reguler = 109, Medium = 209, Large = 409, Category = 3, Description = "Spiciest non veg pizza with spicy" }
            ,new PizzaInventory() { Id = 3, Name = "Farmhouse", Type = 2, Section = 2, Reguler = 119, Medium = 219, Large = 419, Category = 1, Description = "For the veggie gourmet lovers" }
            };

            if(!_context.PizzaInventories.Any())
            _context.PizzaInventories.AddRange(pizzaInventory );

            List<User> userList = new List<User>() {
            new User() { Id = 1, UserName = "admin" , Email = "a@gmail.com" , Password = "a123"},
            new User() { Id = 2, UserName = "customer" , Email = "c@gmail.com" , Password = "c123"}
            };

            if (!_context.Users.Any())
                _context.Users.AddRange( userList );

            List<UserRole> userRoleList = new List<UserRole>() { 
                new UserRole() { Id = 1, UserId = 1, Role = "Admin" },
                new UserRole() { Id = 2, UserId = 2, Role = "Customer" }
            };

            if (!_context.UserRoles.Any())
                _context.UserRoles.AddRange(userRoleList);

            _context.SaveChanges();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
