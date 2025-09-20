using DemoDotnetApi.Data;
using DemoDotnetApi.Endpoints;
using DemoDotnetApi.Extensions;
using DemoDotnetApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
           .EnableSensitiveDataLogging());

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

// Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Migration + Seeder
app.RunMigration();

// Swagger UI (Dev)
//bool isDevelopment = app.Environment.IsDevelopment();
//if (isDevelopment)
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

// Endpoints
app.MapHealthEndpoints();
app.MapWeatherForecastEndpoints();
app.MapGreetingsEndpoints();

app.Run();
