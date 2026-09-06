using BookingApp.Core.Abstractions;
using BookingApp.Infrastructure.Database;
using BookingApp.IntegrationTests.Fakes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace BookingApp.IntegrationTests.Fixtures;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = 
        new PostgreSqlBuilder("postgres:18-alpine")
            .WithHostname("database")
            .WithDatabase("BookingAppDb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
    
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookingAppDbContext>();
        await dbContext.Database.MigrateAsync();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == 
                     typeof(DbContextOptions<BookingAppDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<BookingAppDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
            });
            
            services.RemoveAll<IDateTimeProvider>();
            services.AddSingleton<IDateTimeProvider, TestDateTimeProvider>();
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = TestAuthHandler.DefaultScheme;
                    options.DefaultChallengeScheme = TestAuthHandler.DefaultScheme;
                })
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                    TestAuthHandler.DefaultScheme, options => { });
        });
    }
    
    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}