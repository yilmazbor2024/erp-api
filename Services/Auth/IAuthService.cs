using ErpMobile.Api.Models.Auth;

namespace ErpMobile.Api.Services.Auth;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
