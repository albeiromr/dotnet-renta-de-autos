using Application.Commons.Interfaces;
using Dapper;
using Domain.Commons.Interfaces;
using Domain.Rentals.Interfaces;
using Domain.Users.Interfaces;
using Domain.Vehicles.Interfaces;
using Infrastructure.Databases;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.TypeHandlers;
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

        // agregando los repositorios
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();

        // agregando el unitofwork
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        // injectando la cadena de conexión para el sqlconnectionfactory
        services.AddSingleton<ISqlConnectionFactory>( _ => new SqlConnectionFactory(connectionString));

        // agregando los typehandlers para que postgres pueda soportar tipos de datos que 
        // normalmente no soporta
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        return services;
    }
}
