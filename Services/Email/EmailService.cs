using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace ErpMobile.Api.Services.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly SmtpClient _smtpClient;
    private readonly string _fromEmail;
    private readonly string _fromName;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
        
        var smtpSettings = _configuration.GetSection("SmtpSettings");
        var server = smtpSettings["Server"];
        var port = int.Parse(smtpSettings["Port"] ?? "587");
        var username = smtpSettings["Username"];
        var password = smtpSettings["Password"];
        _fromEmail = smtpSettings["FromEmail"] ?? username ?? "";
        _fromName = smtpSettings["FromName"] ?? "ERP Mobile";

        _smtpClient = new SmtpClient(server, port)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(username, password)
        };
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail, _fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await _smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            // Log the error
            throw new Exception($"Failed to send email: {ex.Message}", ex);
        }
    }
}
