using Microsoft.EntityFrameworkCore;
using UserManagementApp.Application.Services;
using UserManagementApp.Infrastructure.DataAccess.Context;
using UserManagementApp.Infrastructure.DataAccess.Implementations;
using UserManagementApp.Infrastructure.DataAccess.Interceptors;

namespace UserManagementApp.Infrastructure.DataAccess;

public static class ServiceConfigurationExtensions
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<SoftAuditInterceptor>();

        var defaultConnection = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(defaultConnection))
            throw new InvalidOperationException("Default connection string is missing");

        services.AddDbContextPool<AppDbContext>((sp, options) =>
        {
            using var scope = sp.CreateScope();
            var scopedProvider = scope.ServiceProvider;

            options.UseNpgsql(connectionString: defaultConnection);

            options.AddInterceptors(scopedProvider.GetRequiredService<SoftAuditInterceptor>());
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
