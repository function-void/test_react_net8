using UserManagementApp.Application.Services;

namespace UserManagementApp.Infrastructure.Services.Implementations;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTimeOffset TimeOffsetNow => DateTimeOffset.Now;
    public DateTimeOffset TimeOffsetUtcNow => DateTimeOffset.UtcNow;
}
