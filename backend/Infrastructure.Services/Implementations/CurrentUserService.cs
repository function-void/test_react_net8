using UserManagementApp.Application.Services;
using UserManagementApp.Infrastructure.Services.Options;

namespace UserManagementApp.Infrastructure.Services.Implementations;

public class CurrentUserService : ICurrentUserService
{
    public string UserId { get; init; }
    public string CorrelationId { get; init; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, SystemUserOptions systemUserOptions)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value ?? systemUserOptions.Id.ToString();
        CorrelationId = httpContextAccessor.HttpContext?.Request.Headers["x-correlation-id"].ToString() ?? string.Empty;
    }
}