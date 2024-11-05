using WebApiRepositoryPattern;

namespace RepositoryPattern.Services.WeatherForecastService
{
    public interface IWeatherForecastService
    {
        public IEnumerable<WeatherForecast> Get();
    }
}
