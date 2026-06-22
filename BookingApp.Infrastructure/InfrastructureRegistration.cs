using BookingApp.Core.Domain.Rooms.Repositories;
using BookingApp.Core.Domain.RoomTypes.Repositories;
using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Bookings.Repositories;
using BookingApp.Core.Domain.Members.Repositories;
using BookingApp.Infrastructure.Abstractions;
using BookingApp.Infrastructure.Data;
using BookingApp.Infrastructure.Database;
using BookingApp.Infrastructure.Database.Repositories.Bookings;
using BookingApp.Infrastructure.Database.Repositories.Members;
using BookingApp.Infrastructure.Database.Repositories.Rooms;
using BookingApp.Infrastructure.Database.Repositories.RoomTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.Infrastructure;

public static class InfrastructureRegistration
{
    public static IServiceCollection RegisterInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BookingAppDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<PublishDomainEventInterceptor>();

            options.UseNpgsql(configuration.GetConnectionString("BookingApp"))
                .AddInterceptors(interceptor);
        });

        services.AddScoped<IMembersRepository, MembersRepository>();
        services.AddScoped<IRoomsRepository, RoomsRepository>();
        services.AddScoped<IRoomTypesRepository, RoomTypesRepository>();
        services.AddScoped<IBookingsRepository, BookingsRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<BookingAppDbContext>());

        services.AddScoped<PublishDomainEventInterceptor>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}