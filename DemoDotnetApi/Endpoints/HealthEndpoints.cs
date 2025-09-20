using DemoDotnetApi.Data;

namespace DemoDotnetApi.Endpoints;

public static class HealthEndpoints
{
    public static void MapHealthEndpoints(this IEndpointRouteBuilder routes)
    {
        // Create a group with common prefix and metadata
        var group = routes
            .MapGroup("/api/health")
            .WithTags("Health")
            .WithOpenApi();

        group.MapGet("/", (ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger(nameof(HealthEndpoints));
            logger.LogInformation("Health check OK at {Time}", DateTime.UtcNow);

            return Results.Ok(new { status = "Healthy" });
        })
        .WithName("HealthCheck")
        .WithSummary("Check service health")
        .WithDescription("Returns the status of the API service.");

        group.MapGet("/db", async (AppDbContext db, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger(nameof(HealthEndpoints));
            try
            {
                var canConnect = await db.Database.CanConnectAsync();
                if (canConnect)
                {
                    logger.LogInformation("Health check DB OK at {Time}", DateTime.UtcNow);
                    return Results.Ok(new { status = "Healthy", db = "Connected" });
                }
                else
                {
                    logger.LogWarning("Health check failed: DB unreachable at {Time}", DateTime.UtcNow);
                    return Results.Problem("Database unreachable");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Health check DB error at {Time}", DateTime.UtcNow);
                return Results.Problem("Exception during health check");
            }
        })
        .WithName("HealthCheckDb")
        .WithSummary("Check database connectivity")
        .WithDescription("Verifies that the API can connect to the PostgreSQL database.");
    }
}
