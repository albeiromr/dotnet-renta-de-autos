using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

// esta clase se explica en el video número 53 del curso de udemy
// (migración con ef)
// esta clase tiene un método de extensión que nos permite realizar migraciones
public static class ApplyMigrationsExtension
{
    public static async void ApplyMigrations(this IApplicationBuilder app)
    {
        using(var scope = app.ApplicationServices.CreateScope())
        {
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = service.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
            }catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "database Migration error");
            }
        }
    }
}
