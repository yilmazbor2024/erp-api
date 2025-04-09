namespace ErpMobile.Api.Models.Auth;

public class UserInfo
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<string> Roles { get; set; } = new();
}
