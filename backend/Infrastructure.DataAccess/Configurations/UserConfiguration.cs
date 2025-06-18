using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementApp.Domain.Models;

namespace UserManagementApp.Infrastructure.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName).HasMaxLength(512);
        builder.Property(x => x.Role).HasMaxLength(256);
        builder.Property(x => x.Email).HasMaxLength(256);

        builder.ToTable(t => t.HasCheckConstraint("CK_User_Role", "[Role] IN ('Admin', 'Manager', 'User')"));

        builder.Property(x => x.CreatedBy).HasMaxLength(36).IsRequired();
        builder.Property(x => x.DeletedBy).HasMaxLength(36);
        builder.Property(x => x.LastModifiedBy).HasMaxLength(36);
    }
}
