# Demo Dotnet Apis

A .NET 8 Minimal API project with PostgreSQL, EF Core, and Docker Compose for easy development and testing. Includes CRUD for Greetings, Health checks, and WeatherForecast endpoints.

---

## Features

- .NET 8 Minimal API
- Entity Framework Core with PostgreSQL
- CRUD endpoints for Greetings
- Health check endpoints
- WeatherForecast service endpoints
- Logging integrated with `ILogger`
- Swagger / OpenAPI documentation
- Docker Compose setup with PostgreSQL

### Endpoints Overview

| Path | Method | Summary |
|------|--------|--------|
| /api/greetings | GET | Get all greetings |
| /api/greetings | POST | Create a new greeting |
| /api/greetings/{id} | GET | Get a greeting by ID |
| /api/greetings/{id} | PUT | Update a greeting by ID |
| /api/greetings/{id} | DELETE | Delete a greeting by ID |
| /api/health | GET | Check service health |
| /api/health/db | GET | Check database connectivity |
| /api/weatherforecasts | GET | Get weather forecasts |

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/)

---

## Environment Variables

You can define environment variables in a `.env` file or directly in `docker-compose.yml`.  

Example `.env` file:

