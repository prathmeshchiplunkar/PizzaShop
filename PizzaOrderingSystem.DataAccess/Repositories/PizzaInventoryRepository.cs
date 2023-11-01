using Microsoft.EntityFrameworkCore;
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
    public class PizzaInventoryRepository : GenericRepository<PizzaInventory>, IPizzaInventoryRepository
    {
        public PizzaInventoryRepository(PizzaDbContext context, ILogger logger) : base(context, logger)
        {
        }

    }
}
