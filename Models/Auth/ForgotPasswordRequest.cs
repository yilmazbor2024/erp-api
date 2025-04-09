using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Auth;

public class ForgotPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string ClientUrl { get; set; } = null!;
}
