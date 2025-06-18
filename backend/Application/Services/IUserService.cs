using UserManagementApp.Domain.Models;

namespace UserManagementApp.Application.Services;

public interface IUserService
{
    Task CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetAsync(Specification<User> specification, CancellationToken cancellationToken = default);
    void Update(User user);
    void Remove(User user);
}
