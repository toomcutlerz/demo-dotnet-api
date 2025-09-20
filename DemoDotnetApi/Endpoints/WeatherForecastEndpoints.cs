using DemoDotnetApi.Services;

namespace DemoDotnetApi.Endpoints;

public static class WeatherForecastEndpoints
{
    public static void MapWeatherForecastEndpoints(this IEndpointRouteBuilder routes)
    {
        // Create a group with common prefix and metadata
        var group = routes
            .MapGroup("/api/weatherforecasts")
            .WithTags("WeatherForecasts")
            .WithOpenApi();

        group.MapGet("/", (IWeatherForecastService weatherForecastService, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger(nameof(WeatherForecastEndpoints));
            try
            {
                logger.LogInformation("Fetching weather forecasts at {Time}", DateTime.UtcNow);

                var result = weatherForecastService.GetWeatherForecasts();
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fetching weather forecasts error at {Time}", DateTime.UtcNow);
                return Results.Problem("Exception during fetching weather forecasts");
            }

        })
        .WithName("GetWeatherForecasts")
        .WithSummary("Get weather forecasts")
        .WithDescription("Retrieves a list of weather forecasts from the service.");
    }
}
