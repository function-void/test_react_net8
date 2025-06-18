namespace UserManagementApp.Infrastructure.Services.Options;

public sealed class SystemUserOptions
{
    public const string SectionName = "SystemUser";
    public required Guid Id { get; set; }
}
