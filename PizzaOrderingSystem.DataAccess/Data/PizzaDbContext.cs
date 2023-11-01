using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DataAccess.Data
{
    public class PizzaDbContext : DbContext
    {
        public virtual DbSet<PizzaInventory> PizzaInventories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
            : base(options) 
        {
        }        
    }
}
