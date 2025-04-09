using Microsoft.AspNetCore.Identity;

namespace ErpMobile.Api.Entities;

public class Role : IdentityRole
{
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}
