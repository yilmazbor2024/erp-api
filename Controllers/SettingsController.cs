using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ErpMobile.Api.Models.Settings;
using ErpMobile.Api.Services.Email;

namespace ErpMobile.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SettingsController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly ILogger<SettingsController> _logger;
    private readonly SmtpSettings _smtpSettings;

    public SettingsController(
        IEmailService emailService,
        ILogger<SettingsController> logger,
        IOptions<SmtpSettings> smtpSettings)
    {
        _emailService = emailService;
        _logger = logger;
        _smtpSettings = smtpSettings.Value;
    }

    [HttpGet("smtp")]
    public ActionResult<SmtpSettings> GetSmtpSettings()
    {
        // Hassas bilgileri gizle
        var settings = new SmtpSettings
        {
            Server = _smtpSettings.Server,
            Port = _smtpSettings.Port,
            Username = _smtpSettings.Username,
            FromEmail = _smtpSettings.FromEmail,
            FromName = _smtpSettings.FromName,
            EnableSsl = _smtpSettings.EnableSsl,
            UseDefaultCredentials = _smtpSettings.UseDefaultCredentials,
            Timeout = _smtpSettings.Timeout
        };

        return Ok(settings);
    }

    [HttpPost("smtp/test")]
    public async Task<IActionResult> TestSmtpSettings([FromBody] SmtpSettings settings)
    {
        try
        {
            // Test e-postası gönder
            await _emailService.SendEmailAsync(
                settings.FromEmail,
                "SMTP Test Email",
                "This is a test email to verify SMTP settings."
            );

            return Ok(new { message = "Test email sent successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send test email");
            return BadRequest(new { message = ex.Message });
        }
    }
} 