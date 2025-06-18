using Microsoft.EntityFrameworkCore;
using UserManagementApp.Domain.Models;
using UserManagementApp.Infrastructure.DataAccess.Configurations;

namespace UserManagementApp.Infrastructure.DataAccess.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}