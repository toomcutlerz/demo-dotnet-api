using DemoDotnetApi.Data;
using DemoDotnetApi.Services;
using Microsoft.EntityFrameworkCore;

namespace DemoDotnetApi.Extensions;

public static class MigrationExtensions
{
    public static void RunMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            logger.LogInformation("Starting database migration...");
            db.Database.Migrate();

            DbSeeder.Seed(db, logger); // ถ้า seeder ต้อง logger
            logger.LogInformation("Database migration completed.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Migration failed.");
            //throw;
        }
    }
}
