using UserManagementApp.Application.Services;
using UserManagementApp.Infrastructure.DataAccess.Context;

namespace UserManagementApp.Infrastructure.DataAccess.Implementations;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
