using Microsoft.AspNetCore.Builder;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.System.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static void InitialiseDatabase<AppDbContext>(this WebApplication app) where AppDbContext : DbContext
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        initialiser.Initialise(dbContext, app.Environment.IsDevelopment());
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger)
    {
        _logger = logger;
    }

    public void Initialise<AppDbContext>(AppDbContext context, bool isDevelopment) where AppDbContext : DbContext
    {
        try
        {
            var dbContext = (DbContext)context;
            string? connectionString = dbContext.Database.GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                _logger.LogWarning("Database connection string is not configured." + typeof(AppDbContext).Name);
                return;
            }
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string tableCheckQuery = @"SELECT COUNT(*) AS table_count FROM information_schema.tables WHERE table_schema = 'public'";
                using (var command = new NpgsqlCommand(tableCheckQuery, connection))
                {
                    if (Convert.ToInt64(command.ExecuteScalar()) == 0)
                    {
                        _logger.LogInformation("Initialising database...");
                        context.Database.Migrate();
                        _logger.LogInformation("Database initialised successfully");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}
