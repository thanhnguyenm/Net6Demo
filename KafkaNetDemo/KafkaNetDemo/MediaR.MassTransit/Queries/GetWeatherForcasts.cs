using MediatR;

namespace MediaR.MassTransit.Queries
{
    public class GetWeatherForcastsQuery  : IRequest<IEnumerable<WeatherForecast>>
    {

    }

    public class GetWeatherForcastsQueryHanlder : IRequestHandler<GetWeatherForcastsQuery, IEnumerable<WeatherForecast>>
    {
        public async Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForcastsQuery request, CancellationToken cancellationToken)
        {
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Data.Summaries[Random.Shared.Next(Data.Summaries.Length)]
            })
            .ToArray();

            return await Task.FromResult(result);
        }
    }
}
