using Application.Commons.Interfaces;
using Infrastructure.Databases;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // injectando servicios personalizados
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddTransient<IEmailService, EmailService>();

        //obteniendo el connection string de la DB
        var connectionString = configuration.GetConnectionString("database") ??
            throw new ArgumentNullException(nameof(configuration));

        //agregando el db context a los servicios
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            //OJO!!! el método UseSnakeCaseNamingConvention viene de una líbrería
            // independiente para formatear los nombres de las columnas en la db
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        return services;
    }
}
