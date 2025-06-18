using Microsoft.Extensions.Options;
using UserManagementApp.Infrastructure.Services.Options;

namespace UserManagementApp.Options;

public class SystemUserSetup(IConfiguration configuration) : IConfigureOptions<SystemUserOptions>
{
    private readonly IConfiguration _configuration = configuration;

    public void Configure(SystemUserOptions options)
    {
        _configuration.GetSection(SystemUserOptions.SectionName).Bind(options);
    }
}
