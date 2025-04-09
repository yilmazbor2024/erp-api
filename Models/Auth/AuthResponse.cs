using ErpMobile.Api.Models.Menu;

namespace ErpMobile.Api.Models.Auth;

public class AuthResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<string> Roles { get; set; } = new();
    public List<MenuItemDto> MenuItems { get; set; } = new();
}
