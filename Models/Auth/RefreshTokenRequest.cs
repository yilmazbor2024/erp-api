using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Auth;

public class RefreshTokenRequest
{
    [Required]
    public string AccessToken { get; set; } = null!;

    [Required]
    public string RefreshToken { get; set; } = null!;
}
