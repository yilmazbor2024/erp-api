using System;

namespace ErpMobile.Api.Entities;

public class ModulePermission
{
    public Guid Id { get; set; }
    public Guid UserGroupId { get; set; }
    public string ModuleName { get; set; } = null!;
    public bool CanCreate { get; set; }
    public bool CanRead { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanDelete { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }

    public virtual UserGroup UserGroup { get; set; } = null!;
}
