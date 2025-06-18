using UserManagementApp.Application.Services;
using UserManagementApp.Infrastructure.Services.Implementations;

namespace UserManagementApp.Infrastructure.Services;

public static class ServiceConfigurationExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IRequestSender, RequestSender>();

        return services;
    }
}