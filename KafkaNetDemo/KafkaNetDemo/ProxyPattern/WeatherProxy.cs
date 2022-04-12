namespace ProxyPattern
{
    public class WeatherProxy : IWeatherProvider
    {
        private readonly IWeatherProvider weatherProvider;

        public WeatherProxy(IWeatherProvider weatherProvider)
        {
            this.weatherProvider = weatherProvider;
        }
        public IEnumerable<WeatherForecast> GetWeathers(int totalCount)
        {
            if (totalCount > 10) throw new ArgumentException($"total Count must be less than 10");
            return weatherProvider.GetWeathers(totalCount);
        }
    }
}
