using BookingApp.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationRegistration).Assembly;
        
        services.AddValidatorsFromAssembly(assembly);
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        return services;
    }
}