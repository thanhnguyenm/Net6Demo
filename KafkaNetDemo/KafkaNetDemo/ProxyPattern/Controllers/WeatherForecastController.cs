using Microsoft.AspNetCore.Mvc;

namespace ProxyPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherProvider weatherProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherProvider weatherProvider)
        {
            _logger = logger;
            this.weatherProvider = weatherProvider;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get([FromQuery] int total)
        {
            return weatherProvider.GetWeathers(total);
        }
    }
}