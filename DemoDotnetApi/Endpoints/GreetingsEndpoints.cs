using DemoDotnetApi.Data;
using DemoDotnetApi.Models;
using DemoDotnetApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DemoDotnetApi.Endpoints;

public static class GreetingsEndpoints
{
    public static void MapGreetingsEndpoints(this IEndpointRouteBuilder routes)
    {
        // Create a group with common prefix and metadata
        var group = routes
            .MapGroup("/api/greetings")
            .WithTags("Greetings")
            .WithOpenApi();

        // GET all
        group.MapGet("/", async (AppDbContext db, ILogger<Program> logger) =>
        {
            logger.LogInformation("Fetching all greetings at {Time}", DateTime.UtcNow);
            var list = await db.Greetings.ToListAsync();
            logger.LogInformation("Fetched {Count} greetings", list.Count);
            return list;
        })
        .WithName("GetGreetings")
        .WithSummary("Get all greetings")
        .WithDescription("Returns all greetings from the database.");

        // GET by id
        group.MapGet("/{id:int}", async (int id, AppDbContext db, ILogger<Program> logger) =>
        {
            logger.LogInformation("Fetching greeting with ID {Id}", id);
            var greeting = await db.Greetings.FindAsync(id);
            if (greeting is null)
            {
                logger.LogWarning("Greeting with ID {Id} not found", id);
                return Results.NotFound();
            }
            logger.LogInformation("Greeting with ID {Id} found", id);
            return Results.Ok(greeting);
        })
        .WithName("GetGreetingById")
        .WithSummary("Get a greeting by ID")
        .WithDescription("Returns a single greeting identified by ID.");

        // POST create
        group.MapPost("/", async (AddGreetingRequest request, AppDbContext db, ILogger<Program> logger) =>
        {
            logger.LogInformation("Creating new greeting: {Message}", request.Message);
            var greeting = new Greeting { Message = request.Message };
            db.Greetings.Add(greeting);
            await db.SaveChangesAsync();
            logger.LogInformation("Greeting created with ID {Id}", greeting.Id);
            return Results.Created($"/greetings/{greeting.Id}", greeting);
        })
        .WithName("CreateGreeting")
        .WithSummary("Create a new greeting")
        .WithDescription("Adds a new greeting to the database.");

        // PUT update
        group.MapPut("/{id:int}", async (int id, UpdateGreetingRequest request, AppDbContext db, ILogger<Program> logger) =>
        {
            logger.LogInformation("Updating greeting with ID {Id}", id);
            var greeting = await db.Greetings.FindAsync(id);
            if (greeting is null)
            {
                logger.LogWarning("Greeting with ID {Id} not found for update", id);
                return Results.NotFound();
            }

            greeting.Message = request.Message;
            await db.SaveChangesAsync();
            logger.LogInformation("Greeting with ID {Id} updated to: {Message}", id, request.Message);
            return Results.Ok(greeting);
        })
        .WithName("UpdateGreeting")
        .WithSummary("Update a greeting by ID")
        .WithDescription("Updates the message of an existing greeting.");

        // DELETE
        group.MapDelete("/{id:int}", async (int id, AppDbContext db, ILogger<Program> logger) =>
        {
            logger.LogInformation("Deleting greeting with ID {Id}", id);
            var greeting = await db.Greetings.FindAsync(id);
            if (greeting is null)
            {
                logger.LogWarning("Greeting with ID {Id} not found for deletion", id);
                return Results.NotFound();
            }

            db.Greetings.Remove(greeting);
            await db.SaveChangesAsync();
            logger.LogInformation("Greeting with ID {Id} deleted", id);
            return Results.NoContent();
        })
        .WithName("DeleteGreeting")
        .WithSummary("Delete a greeting by ID")
        .WithDescription("Removes a greeting from the database by its ID.");
    }
}
