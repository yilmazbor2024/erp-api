using System;
using System.Threading.Tasks;
using ErpMobile.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auditlog")]
    [Authorize]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;
        
        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }
        
        [HttpPost("log-page-view")]
        public async Task<IActionResult> LogPageView([FromBody] PageViewLogDto logDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(username))
                {
                    return Unauthorized();
                }
                
                // Kullanıcı tarafından gönderilen username varsa onu kullan
                string logUsername = !string.IsNullOrEmpty(logDto.Username) ? logDto.Username : username;
                
                // Tarih alanlarını parse et
                DateTime? visitTime = null;
                DateTime? exitTime = null;
                
                if (!string.IsNullOrEmpty(logDto.VisitTime))
                {
                    if (DateTime.TryParse(logDto.VisitTime, out DateTime parsedVisitTime))
                    {
                        visitTime = parsedVisitTime;
                    }
                }
                
                if (!string.IsNullOrEmpty(logDto.ExitTime))
                {
                    if (DateTime.TryParse(logDto.ExitTime, out DateTime parsedExitTime))
                    {
                        exitTime = parsedExitTime;
                    }
                }
                
                var logId = await _auditLogService.LogPageViewAsync(
                    userId,
                    logUsername,
                    logDto.PageUrl,
                    logDto.Module,
                    visitTime,
                    exitTime,
                    logDto.Duration
                );
                
                return Ok(new { LogId = logId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Loglama işlemi sırasında bir hata oluştu", Error = ex.Message });
            }
        }
        
        [HttpPost("log-form-action")]
        public async Task<IActionResult> LogFormAction([FromBody] FormActionLogDto logDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(username))
                {
                    return Unauthorized();
                }
                
                var logId = await _auditLogService.LogFormActionAsync(
                    userId,
                    username,
                    logDto.FormName,
                    logDto.Action,
                    logDto.Details
                );
                
                return Ok(new { LogId = logId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Form işlemi loglama sırasında bir hata oluştu", Error = ex.Message });
            }
        }
        
        [HttpPost("log-api-call")]
        [AllowAnonymous]
        public async Task<IActionResult> LogApiCall([FromBody] ApiCallLogDto logDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                
                // Eğer kullanıcı kimliği doğrulanamadıysa ve frontend'den username gönderilmişse onu kullan
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(username))
                {
                    if (string.IsNullOrEmpty(logDto.Username))
                    {
                        return Unauthorized();
                    }
                    
                    userId = "anonymous";
                    username = logDto.Username;
                }
                
                // Kullanıcı tarafından gönderilen username varsa onu kullan
                string logUsername = !string.IsNullOrEmpty(logDto.Username) ? logDto.Username : username;
                
                // IP adresi yoksa request'ten almaya çalış
                string ipAddress = logDto.IPAddress;
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                }
                
                var logId = await _auditLogService.LogApiCallAsync(
                    userId,
                    logUsername,
                    logDto.Endpoint,
                    logDto.Method,
                    logDto.Status,
                    logDto.ResponseTime,
                    logDto.Details,
                    logDto.UserAgent,
                    logDto.Browser,
                    logDto.OS,
                    logDto.Device,
                    ipAddress,
                    logDto.Location
                );
                
                return Ok(new { LogId = logId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "API çağrısı loglama işlemi sırasında bir hata oluştu", Error = ex.Message });
            }
        }
        
        [HttpGet("user-logs")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserLogs(string userId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var logs = await _auditLogService.GetUserLogsAsync(userId, startDate, endDate);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Kullanıcı logları alınırken bir hata oluştu", Error = ex.Message });
            }
        }
        
        [HttpGet("all-logs")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllLogs(
            string module = null, 
            string action = null, 
            string username = null,
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            int page = 1, 
            int pageSize = 20)
        {
            try
            {
                var (logs, totalCount) = await _auditLogService.GetAllLogsAsync(
                    module, action, username, startDate, endDate, page, pageSize);
                
                return Ok(new { 
                    Logs = logs,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Loglar alınırken bir hata oluştu", Error = ex.Message });
            }
        }
    }
    
    public class PageViewLogDto
    {
        public string PageUrl { get; set; }
        public string Module { get; set; }
        public string Username { get; set; }
        public string VisitTime { get; set; }
        public string ExitTime { get; set; }
        public int? Duration { get; set; }
        public string UserAgent { get; set; }
        public string Browser { get; set; }
        public string OS { get; set; }
        public string Device { get; set; }
        public string IPAddress { get; set; }
        public string Location { get; set; }
    }
    
    public class FormActionLogDto
    {
        public string FormName { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public string UserAgent { get; set; }
        public string Browser { get; set; }
        public string OS { get; set; }
        public string Device { get; set; }
        public string IPAddress { get; set; }
        public string Location { get; set; }
    }
    
    public class ApiCallLogDto
    {
        public string Endpoint { get; set; }
        public string Method { get; set; }
        public int? Status { get; set; }
        public int? ResponseTime { get; set; }
        public string Details { get; set; }
        public string Username { get; set; }
        public string UserAgent { get; set; }
        public string Browser { get; set; }
        public string OS { get; set; }
        public string Device { get; set; }
        public string IPAddress { get; set; }
        public string Location { get; set; }
    }
}
