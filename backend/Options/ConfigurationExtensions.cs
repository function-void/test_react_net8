using Microsoft.Extensions.Options;
using UserManagementApp.Infrastructure.Services.Options;

namespace UserManagementApp.Options;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddConfigureOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<SystemUserSetup>();

        services.AddSingleton(x => x.GetService<IOptions<SystemUserOptions>>()!.Value);

        return services;
    }
}
