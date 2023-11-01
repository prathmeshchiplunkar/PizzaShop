using PizzaOrderingSystem.DataAccess.Models;
using PizzaOrderingSystem.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.DTO.IService
{
    public interface IPizzaInventoryService
    {
        public Task<IEnumerable<PizzaInventoryDTO>> GetPizzaInventoryAsync();
        public Task<bool> InsertAsync(PizzaInventoryDTO pizzaInventoryDTO);
        public Task<int> CompletedAsync();
    }
}
