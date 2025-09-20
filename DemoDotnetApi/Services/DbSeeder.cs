using DemoDotnetApi.Data;
using DemoDotnetApi.Models;

namespace DemoDotnetApi.Services;

public static class DbSeeder
{
    public static void Seed(AppDbContext db, ILogger logger)
    {
        // Clear existing data
        // db.Greetings.RemoveRange(context.Greetings);
        // db.SaveChanges();

        if (!db.Greetings.Any())
        {
            db.Greetings.AddRange(
                new Greeting { Message = "Hello Weather Forecast APIs" },
                new Greeting { Message = "Hello WebAPIs .NET + Postgres" }
            );
            db.SaveChanges();

            logger.LogInformation("Seeded greetings table.");
        }
    }
}
