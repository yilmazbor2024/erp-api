namespace ErpMobile.Api.Entities;

public class AuditLog
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string TableName { get; set; } = null!;
    public string? PrimaryKey { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? AffectedColumns { get; set; }
    public DateTime CreatedAt { get; set; }
}
