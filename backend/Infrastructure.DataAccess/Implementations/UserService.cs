using Microsoft.EntityFrameworkCore;
using UserManagementApp.Application.Features.Users.Responses;
using UserManagementApp.Application.Services;
using UserManagementApp.Domain.Models;
using UserManagementApp.Infrastructure.DataAccess.Context;

namespace UserManagementApp.Infrastructure.DataAccess.Implementations;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken) ;
    }

    public Task<UserDto[]> GetUsersAsync(string? search, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }
}
