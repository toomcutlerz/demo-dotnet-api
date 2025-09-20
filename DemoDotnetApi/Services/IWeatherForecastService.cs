using DemoDotnetApi.ViewModels;

namespace DemoDotnetApi.Services;

public interface IWeatherForecastService
{
    WeatherForecast[] GetWeatherForecasts();
}
