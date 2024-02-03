using Application.Commons.Behaviors;
using Domain.Rentals.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => { 

            // agregando los commands y los queries
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);

            // agregando los behaviors
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        //injectando todas los validators desde el assemby
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        //injectando servicios personalizados
        services.AddTransient<PriceService>();

        return services;
    }
}
