using DemoDotnetApi.EntityConfigurations;
using DemoDotnetApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoDotnetApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Greeting> Greetings => Set<Greeting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Auto apply all EntityConfigurations from assembly
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.ApplyConfiguration(new GreetingConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}



