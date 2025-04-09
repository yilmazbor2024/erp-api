namespace ErpMobile.Api.Entities;

public class UserSession
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public string IpAddress { get; set; } = null!;
    public string DeviceInfo { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime LastActivity { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public virtual User User { get; set; } = null!;
}
