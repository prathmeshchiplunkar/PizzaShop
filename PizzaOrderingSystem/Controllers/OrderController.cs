using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.DTO.Models;

namespace PizzaOrderingSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllOrders")]
        public IEnumerable<OrderDTO> GetAllOrders()
        {
            //_service.GetPizzaInventoryAsync().Wait();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
            return null;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllOrderById")]
        public OrderDTO GetAllOrderById(int id)
        {            
            return null;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetTodaysOrders")]
        public IEnumerable<OrderDTO> GetTodaysOrders()
        {
            return null;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetInProcessOrders")]
        public IEnumerable<OrderDTO> GetOrdersByStstus(string orderStatus)
        {
            return null;
        }
    }
}
