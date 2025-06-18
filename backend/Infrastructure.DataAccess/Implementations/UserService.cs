using Microsoft.EntityFrameworkCore;
using UserManagementApp.Application.Services;
using UserManagementApp.Domain.Models;
using UserManagementApp.Infrastructure.DataAccess.Context;

namespace UserManagementApp.Infrastructure.DataAccess.Implementations;

public class UserService(AppDbContext context) : IUserService
{
    private readonly AppDbContext _context = context;

    public async Task CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }

    public async Task<User?> GetAsync(Specification<User> specification, CancellationToken cancellationToken = default)
    {
        return await SpecificationQueryBuilder.BuildQuery(source: _context.Users, specification: specification).FirstOrDefaultAsync(cancellationToken);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }
}
