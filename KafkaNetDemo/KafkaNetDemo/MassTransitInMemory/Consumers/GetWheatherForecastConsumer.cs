using MassTransit;

namespace MassTransitInMemory.Consumers
{
    public interface IMessage { }

    public abstract class BaseMessage : IMessage { }

    public class GetWeatherForcastsMessage : BaseMessage
    {

    }

    public class GetWeatherForcastsResult
    {
        public IEnumerable<WeatherForecast> WeatherForecasts { get; set; }
    }

    public class GetWheatherForecastConsumer : IConsumer<GetWeatherForcastsMessage>
    {
        public Task Consume(ConsumeContext<GetWeatherForcastsMessage> context)
        {
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Data.Summaries[Random.Shared.Next(Data.Summaries.Length)]
            })
            .ToArray();

            context.RespondAsync<GetWeatherForcastsResult>(new GetWeatherForcastsResult
            {
                WeatherForecasts = result
            });

            return Task.CompletedTask;
        }
    }
}
