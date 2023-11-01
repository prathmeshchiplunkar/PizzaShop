using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.DTO.IService;
using PizzaOrderingSystem.DTO.Models;

namespace PizzaOrderingSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly ILogger<PizzaController> _logger;
        private readonly IPizzaInventoryService _pizzaInventoryService;
        public PizzaController(ILogger<PizzaController> logger, IPizzaInventoryService pizzaInventoryService ) 
        {
            _logger = logger;
            _pizzaInventoryService = pizzaInventoryService;
        }

        [HttpGet("GetPizzaDetails")]
        public Task<IEnumerable<PizzaInventoryDTO>> GetPizzaDetails()
        {
            return _pizzaInventoryService.GetPizzaInventoryAsync();         
        }

        [HttpGet("GetIngredients")]
        public Task<IEnumerable<IngredientDTO>> GetIngredients()
        {
            return null;
        }

    }
}
