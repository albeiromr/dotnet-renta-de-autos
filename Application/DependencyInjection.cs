using Application.Commons.Behaviors;
using Domain.Rentals.Services;
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

        services.AddTransient<PriceService>();

        return services;
    }
}
