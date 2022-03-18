using MassTransit;
using MassTransitInMemory.Consumers;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitInMemory.Controllers
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
        private readonly IBus _bus;
        private readonly IRequestClient<IMessage> _client;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBus bus, IRequestClient<IMessage> client)
        {
            _logger = logger;
            _bus = bus;
            _client = client;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();

            await _bus.Publish(new GetWeatherForcastsMessage()); //cannot get reponse

            //var rs = await _client.GetResponse<GetWeatherForcastsResult>(new GetWeatherForcastsMessage());
            return await Task.FromResult(new List<WeatherForecast>());
        }
    }
}