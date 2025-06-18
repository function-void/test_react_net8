namespace UserManagementApp.Application.Services;

public interface IUnitOfWork
{
    bool SaveChanges();
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}
