using UserManagementApp.Application.Services;
using UserManagementApp.Domain.Models;

namespace UserManagementApp.Infrastructure.DataAccess.Implementations;

public class RoleService : IRoleService
{
    private readonly IReadOnlyList<string> _roles =
    [
        "Admin",
        "User",
        "Manager"
    ];

    public Result<bool> CheckRole(string role)
    {
        return _roles.Contains(role)
            ? true
            : new Error(System.Net.HttpStatusCode.BadRequest, "Role not found.");
    }
}
