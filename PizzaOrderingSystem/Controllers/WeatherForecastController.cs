using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using NLog.Filters;
using PizzaOrderingSystem.DataAccess.Models;
using PizzaOrderingSystem.DTO.IService;
using PizzaOrderingSystem.Utils.Exceptions;
using PizzaOrderingSystem.Utils.Filters;

namespace PizzaOrderingSystem.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    //[ServiceFilter(typeof(JWTActionFilter))] 
    public class WeatherForecastController : ControllerBase
    {
        private readonly IPizzaInventoryService _service;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPizzaInventoryService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetWeatherData")]
        public IEnumerable<WeatherForecast> GetWeatherData()
        {
            _service.GetPizzaInventoryAsync().Wait();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("GetWeatherCondition")]
        public IEnumerable<WeatherForecast> GetWeatherCondition()
        {
            _service.GetPizzaInventoryAsync().Wait();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}