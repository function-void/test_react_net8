using UserManagementApp.Application.Features.Users.Responses;
using UserManagementApp.Domain.Models;

namespace UserManagementApp.Application.Services;

public interface IUserService
{
    Task CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Update(User user);
    void Remove(User user);
    Task<UserDto[]> GetUsersAsync(string? search, CancellationToken cancellationToken = default);
}
