using System.Security.Claims;
using ErpMobile.Api.Entities;

namespace ErpMobile.Api.Services.Auth;

public interface IJwtService
{
    string GenerateAccessToken(User user, IList<string> roles);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
