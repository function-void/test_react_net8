namespace UserManagementApp.Domain.Interfaces;

public interface ISoftAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime? LastModifiedAt { get; set; }
    DateTime? DeletedAt { get; set; }
    string CreatedBy { get; set; }
    string? LastModifiedBy { get; set; }
    string? DeletedBy { get; set; }
    bool IsDeleted { get; set; }
}
