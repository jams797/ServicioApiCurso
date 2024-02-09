using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace ServicioApiCurso.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // UsuarioCreado //PascalCase
            dynamic TimeNow = new DateTime(2024, 12, 1);
            DateTime TimeModif = TimeNow.AddMonths(-1);
            return Enumerable.Range(1, 10).Select((index) => FuncionPrueba(index))
            .ToArray();
        }

        public WeatherForecast FuncionPrueba(int index)
        {
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                Name = "Hola",
            };
        }
    }
}
