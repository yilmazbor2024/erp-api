using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ErpMobile.Api.Models.Auth;
using ErpMobile.Api.Entities;
using ErpMobile.Api.Services.Auth;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, UserManager<User> userManager, ILogger<AuthController> logger)
    {
        _authService = authService;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Null argument provided");
            return BadRequest("Invalid request data");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return BadRequest("Bu email adresi zaten kullanılıyor");
            }

            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Varsayılan olarak User rolünü ata
            await _userManager.AddToRoleAsync(user, "User");

            return Ok("Kullanıcı başarıyla oluşturuldu");
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Null argument provided");
            return BadRequest("Invalid request data");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during register");
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        try
        {
            // JWT token kullanıldığı için backend'de özel bir işlem yapmaya gerek yok
            // Client tarafında token'ı silmek yeterli
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during logout");
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Şifre başarıyla değiştirildi");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during password change");
            return BadRequest(ex.Message);
        }
    }
}
