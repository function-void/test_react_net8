using System.ComponentModel.DataAnnotations;
using UserManagementApp.Domain.Interfaces;

namespace UserManagementApp.Domain.Models;

public abstract class SoftAuditableEntity : ISoftAuditable
{
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string? LastModifiedBy { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}
