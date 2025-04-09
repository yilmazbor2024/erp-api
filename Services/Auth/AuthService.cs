using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ErpMobile.Api.Models.Auth;
using ErpMobile.Api.Entities;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(UserManager<User> userManager, IConfiguration configuration, ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Login attempt for email: {Email}", request.Email);

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogWarning("User not found for email: {Email}", request.Email);
                throw new Exception("Invalid email or password");
            }

            _logger.LogInformation("User found: {UserId}, Email: {Email}, PasswordHash: {PasswordHash}", 
                user.Id, user.Email, user.PasswordHash);

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            
            _logger.LogInformation("Password check result for {Email}: {IsValid}", user.Email, isPasswordValid);

            if (!isPasswordValid)
            {
                _logger.LogWarning("Invalid password for user: {Email}", request.Email);
                throw new Exception("Invalid email or password");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            
            _logger.LogInformation("User roles for {Email}: {Roles}", user.Email, string.Join(", ", userRoles));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                _logger.LogError("JWT key is not configured");
                throw new Exception("JWT key is not configured");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            _logger.LogInformation("Login successful for user: {Email}", request.Email);

            return new LoginResponse
            {
                Token = tokenString,
                Expiration = expires
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for email: {Email}", request?.Email);
            throw;
        }
    }
}
