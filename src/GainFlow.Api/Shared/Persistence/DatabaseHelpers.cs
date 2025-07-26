using GainFlow.Api.Shared.Persistence.Seeders;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Shared.Persistence;

public static class DatabaseHelpers
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        IdentityApplicationDbContext identityContext =
            scope.ServiceProvider.GetRequiredService<IdentityApplicationDbContext>();
        ApplicationDbContext applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            await identityContext.Database.MigrateAsync();
            await applicationContext.Database.MigrateAsync();
            app.Logger.LogInformation("Migrations applied");
        }
        catch (Exception e)
        {
            app.Logger.LogError(e, "Error applying migrations");
            throw;
        }
    }

    public static async Task SeedDatabase(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        ApplicationDbContext applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            await applicationContext.Database.EnsureCreatedAsync();

            if ((await applicationContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await applicationContext.Database.MigrateAsync();
                app.Logger.LogInformation("Migrations applied");
            }

            await ExerciseSeeder.SeedAsync(applicationContext);
            app.Logger.LogInformation("Database seeded successfully");
        }
        catch (Exception e)
        {
            app.Logger.LogError(e, "Error occured while seeding database");
            throw;
        }
    }
}
