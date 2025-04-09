using Microsoft.AspNetCore.Identity;

namespace ErpMobile.Api.Entities;

public class User : IdentityUser
{
    public string UserCode { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string? LastLoginIp { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    
    public virtual ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();
    
    // Navigation properties
    public UserGroup? UserGroup { get; set; }
}
