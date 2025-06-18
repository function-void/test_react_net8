using UserManagementApp.Domain.Models;

namespace UserManagementApp.Application.Services;

public interface IRoleService
{
    Result<bool> CheckRole(string role);
}
