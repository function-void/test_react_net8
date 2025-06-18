namespace UserManagementApp.Application.Services;

public interface ICurrentUserService
{
    string UserId { get; init; }
    string CorrelationId { get; init; }
}
