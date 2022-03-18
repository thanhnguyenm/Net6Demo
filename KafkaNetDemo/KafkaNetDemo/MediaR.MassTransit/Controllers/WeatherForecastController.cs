using MassTransit;
using MediaR.MassTransit.Consumers;
using MediaR.MassTransit.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MediaR.MassTransit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;
        private readonly IRequestClient<IMessage> _client;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator, IRequestClient<IMessage> client)
        {
            _logger = logger;
            _mediator = mediator;
            _client = client;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Data.Summaries[Random.Shared.Next(Data.Summaries.Length)]
            //})
            //.ToArray();


            //return await _mediator.Send(new GetWeatherForcastsQuery());

            return (await _client.GetResponse<GetWeatherForcastsResult>(new GetWeatherForcastsMessage { })).Message.WeatherForecasts;

        }
    }
}