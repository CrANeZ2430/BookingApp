using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(ApplicationRegistration).Assembly));

        return services;
    }
}