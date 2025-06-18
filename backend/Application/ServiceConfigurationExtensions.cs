using Scrutor;
using System.Reflection;
using FluentValidation;
using UserManagementApp.Application.Interfaces;

namespace UserManagementApp.Application;

public static class ServiceConfigurationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssembly(assembly);

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
            .UsingRegistrationStrategy(RegistrationStrategy.Throw)
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );

        return services;
    }
}
