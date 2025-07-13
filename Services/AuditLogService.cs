using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Auth;
using ErpMobile.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ErpMobile.Api.Entities;

namespace ErpMobile.Api.Services
{
    public interface IAuditLogService
    {
        Task<int> LogPageViewAsync(string userId, string username, string pageUrl, string module, DateTime? visitTime = null, DateTime? exitTime = null, int? duration = null, string userAgent = null, string browser = null, string os = null, string device = null, string ipAddress = null, string location = null);
        Task<int> LogFormActionAsync(string userId, string username, string formName, string action, string details, string userAgent = null, string browser = null, string os = null, string device = null, string ipAddress = null, string location = null);
        Task<int> LogApiCallAsync(string userId, string username, string endpoint, string method, string details);
        Task<int> LogApiCallAsync(string userId, string username, string endpoint, string method, int? status, int? responseTime, string details, string userAgent = null, string browser = null, string os = null, string device = null, string ipAddress = null, string location = null);
        Task<IEnumerable<Models.Auth.AuditLog>> GetUserLogsAsync(string userId, DateTime? startDate, DateTime? endDate);
        Task<(IEnumerable<Models.Auth.AuditLog> logs, int totalCount)> GetAllLogsAsync(string module, string action, string username, DateTime? startDate, DateTime? endDate, int page, int pageSize);
    }

    public class AuditLogService : IAuditLogService
    {
        private readonly NanoServiceDbContext _context;

        public AuditLogService(NanoServiceDbContext context)
        {
            _context = context;
        }

        public async Task<int> LogPageViewAsync(string userId, string username, string pageUrl, string module, DateTime? visitTime = null, DateTime? exitTime = null, int? duration = null, string userAgent = null, string browser = null, string os = null, string device = null, string ipAddress = null, string location = null)
        {
            var logModel = new Models.Auth.AuditLog
            {
                UserId = userId,
                Username = username,
                Action = "PAGE_VIEW",
                PageUrl = pageUrl,
                Details = "",
                Timestamp = visitTime ?? DateTime.Now,
                Module = module,
                IpAddress = ipAddress ?? "",
                UserAgent = userAgent ?? ""
            };

            // Giriş-çıkış zamanı, süre ve cihaz bilgilerini JSON olarak kaydet
            var detailsObj = new Dictionary<string, object>
            {
                { "VisitTime", visitTime?.ToString("yyyy-MM-dd HH:mm:ss") },
                { "ExitTime", exitTime?.ToString("yyyy-MM-dd HH:mm:ss") },
                { "Duration", duration }
            };
            
            // Kullanıcı, tarayıcı, bilgisayar ve konum detaylarını ekle
            if (!string.IsNullOrEmpty(userAgent)) detailsObj["userAgent"] = userAgent;
            if (!string.IsNullOrEmpty(browser)) detailsObj["browser"] = browser;
            if (!string.IsNullOrEmpty(os)) detailsObj["os"] = os;
            if (!string.IsNullOrEmpty(device)) detailsObj["device"] = device;
            if (!string.IsNullOrEmpty(ipAddress)) detailsObj["ipAddress"] = ipAddress;
            if (!string.IsNullOrEmpty(location)) detailsObj["location"] = location;
            
            logModel.Details = System.Text.Json.JsonSerializer.Serialize(detailsObj);

            var log = new Entities.AuditLog
            {
                UserId = userId,
                Type = "PAGE_VIEW",
                TableName = module,
                PrimaryKey = pageUrl,
                NewValues = logModel.Details,
                CreatedAt = visitTime ?? DateTime.Now
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
            return (int)log.Id.GetHashCode();
        }

        public async Task<int> LogFormActionAsync(string userId, string username, string formName, string action, string details, string userAgent = null, string browser = null, string os = null, string device = null, string ipAddress = null, string location = null)
        {
            // Form işlemi detaylarını JSON formatında kaydet
            string formDetails = details;
            if (!string.IsNullOrEmpty(userAgent) || !string.IsNullOrEmpty(browser) || !string.IsNullOrEmpty(os) || !string.IsNullOrEmpty(device) || !string.IsNullOrEmpty(ipAddress) || !string.IsNullOrEmpty(location))
            {
                // Eğer details zaten JSON formatındaysa, içeriğini koruyarak yeni alanlar ekle
                try
                {
                    var existingDetails = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(details);
                    
                    // Kullanıcı, tarayıcı, bilgisayar ve konum detaylarını ekle
                    if (!string.IsNullOrEmpty(userAgent)) existingDetails["userAgent"] = userAgent;
                    if (!string.IsNullOrEmpty(browser)) existingDetails["browser"] = browser;
                    if (!string.IsNullOrEmpty(os)) existingDetails["os"] = os;
                    if (!string.IsNullOrEmpty(device)) existingDetails["device"] = device;
                    if (!string.IsNullOrEmpty(ipAddress)) existingDetails["ipAddress"] = ipAddress;
                    if (!string.IsNullOrEmpty(location)) existingDetails["location"] = location;
                    
                    formDetails = System.Text.Json.JsonSerializer.Serialize(existingDetails);
                }
                catch
                {
                    // JSON parse edilemezse, yeni bir JSON oluştur
                    formDetails = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        userAgent = userAgent,
                        browser = browser,
                        os = os,
                        device = device,
                        ipAddress = ipAddress,
                        location = location,
                        details = details
                    });
                }
            }
            
            var logModel = new Models.Auth.AuditLog
            {
                UserId = userId,
                Username = username,
                Action = "FORM_" + action,
                PageUrl = formName,
                Details = formDetails,
                Timestamp = DateTime.Now,
                Module = GetModuleFromFormName(formName),
                IpAddress = ipAddress ?? "",
                UserAgent = userAgent ?? ""
            };

            var log = new Entities.AuditLog
            {
                UserId = userId,
                Type = "FORM_" + action,
                TableName = GetModuleFromFormName(formName),
                PrimaryKey = formName,
                NewValues = formDetails,
                CreatedAt = DateTime.Now
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
            return (int)log.Id.GetHashCode();
        }

        public async Task<int> LogApiCallAsync(string userId, string username, string endpoint, string method, string details)
        {
            var logModel = new Models.Auth.AuditLog
            {
                UserId = userId,
                Username = username,
                Action = "API_" + method,
                PageUrl = endpoint,
                Details = details,
                Timestamp = DateTime.Now,
                Module = GetModuleFromEndpoint(endpoint),
                IpAddress = "",
                UserAgent = ""
            };

            var log = new Entities.AuditLog
            {
                UserId = userId,
                Type = "API_" + method,
                TableName = GetModuleFromEndpoint(endpoint),
                PrimaryKey = endpoint,
                NewValues = details,
                CreatedAt = DateTime.Now
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
            return (int)log.Id.GetHashCode();
        }
        
        public async Task<int> LogApiCallAsync(string userId, string username, string endpoint, string method, int? status, int? responseTime, string details, string userAgent = null, string browser = null, string os = null, string device = null, string ipAddress = null, string location = null)
        {
            // API cevaplarının boyutunu agresif şekilde sınırlandır
            string limitedDetails = LimitApiResponseSize(details);
            
            // API çağrısı detaylarını JSON formatında kaydet
            string apiDetails = limitedDetails;
            if (status.HasValue || responseTime.HasValue || !string.IsNullOrEmpty(userAgent) || !string.IsNullOrEmpty(browser) || !string.IsNullOrEmpty(os) || !string.IsNullOrEmpty(device) || !string.IsNullOrEmpty(ipAddress) || !string.IsNullOrEmpty(location))
            {
                // Eğer details zaten JSON formatındaysa, içeriğini koruyarak yeni alanlar ekle
                try
                {
                    var existingDetails = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(limitedDetails);
                    existingDetails["status"] = status;
                    existingDetails["responseTime"] = responseTime;
                    
                    // Kullanıcı, tarayıcı, bilgisayar ve konum detaylarını ekle
                    if (!string.IsNullOrEmpty(userAgent)) existingDetails["userAgent"] = userAgent;
                    if (!string.IsNullOrEmpty(browser)) existingDetails["browser"] = browser;
                    if (!string.IsNullOrEmpty(os)) existingDetails["os"] = os;
                    if (!string.IsNullOrEmpty(device)) existingDetails["device"] = device;
                    if (!string.IsNullOrEmpty(ipAddress)) existingDetails["ipAddress"] = ipAddress;
                    if (!string.IsNullOrEmpty(location)) existingDetails["location"] = location;
                    
                    apiDetails = System.Text.Json.JsonSerializer.Serialize(existingDetails);
                }
                catch
                {
                    // JSON parse edilemezse, yeni bir JSON oluştur
                    apiDetails = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        status = status,
                        responseTime = responseTime,
                        userAgent = userAgent,
                        browser = browser,
                        os = os,
                        device = device,
                        ipAddress = ipAddress,
                        location = location,
                        details = limitedDetails
                    });
                }
            }
            
            var logModel = new Models.Auth.AuditLog
            {
                UserId = userId,
                Username = username,
                Action = "API_" + method,
                PageUrl = endpoint,
                Details = apiDetails,
                Timestamp = DateTime.Now,
                Module = GetModuleFromEndpoint(endpoint),
                IpAddress = ipAddress ?? "",
                UserAgent = userAgent ?? ""
            };

            var log = new Entities.AuditLog
            {
                UserId = userId,
                Type = "API_" + method,
                TableName = GetModuleFromEndpoint(endpoint),
                PrimaryKey = endpoint,
                NewValues = apiDetails,
                CreatedAt = DateTime.Now
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
            return (int)log.Id.GetHashCode();
        }

        public async Task<IEnumerable<Models.Auth.AuditLog>> GetUserLogsAsync(string userId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.AuditLogs.Where(l => l.UserId == userId);

            if (startDate.HasValue)
                query = query.Where(l => l.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(l => l.CreatedAt <= endDate.Value);

            var logs = await query.OrderByDescending(l => l.CreatedAt).ToListAsync();
            
            // Entity'den model'e dönüştür
            return logs.Select(l => new Models.Auth.AuditLog
            {
                Id = (int)l.Id.GetHashCode(),
                UserId = l.UserId,
                Username = l.UserId, // Entity'de username yok, UserId kullanılıyor
                Action = l.Type,
                Module = l.TableName,
                PageUrl = l.PrimaryKey,
                FormName = l.TableName,
                Details = l.NewValues,
                IpAddress = "",
                UserAgent = "",
                Timestamp = l.CreatedAt
            }).ToList();
        }

        public async Task<(IEnumerable<Models.Auth.AuditLog> logs, int totalCount)> GetAllLogsAsync(
            string module, string action, string username, DateTime? startDate, DateTime? endDate, int page, int pageSize)
        {
            var query = _context.AuditLogs.AsQueryable();

            if (!string.IsNullOrEmpty(module))
                query = query.Where(l => l.TableName == module);

            if (!string.IsNullOrEmpty(action))
                query = query.Where(l => l.Type == action);

            if (!string.IsNullOrEmpty(username))
                query = query.Where(l => l.UserId.Contains(username));

            if (startDate.HasValue)
                query = query.Where(l => l.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(l => l.CreatedAt <= endDate.Value);

            var totalCount = await query.CountAsync();

            var entityLogs = await query
                .OrderByDescending(l => l.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
                
            // Entity'den model'e dönüştür
            var logs = entityLogs.Select(l => new Models.Auth.AuditLog
            {
                Id = (int)l.Id.GetHashCode(),
                UserId = l.UserId,
                Username = l.UserId, // Entity'de username yok, UserId kullanılıyor
                Action = l.Type,
                Module = l.TableName,
                PageUrl = l.PrimaryKey,
                FormName = l.TableName,
                Details = l.NewValues,
                IpAddress = "",
                UserAgent = "",
                Timestamp = l.CreatedAt
            }).ToList();

            return (logs, totalCount);
        }

        private string GetModuleFromFormName(string formName)
        {
            // Form adından modül adını çıkar
            if (formName.Contains("Invoice"))
                return "Invoice";
            if (formName.Contains("Customer"))
                return "Customer";
            if (formName.Contains("Product"))
                return "Product";
            // Diğer modüller için benzer kontroller
            
            return "Other";
        }

        private string GetModuleFromEndpoint(string endpoint)
        {
            if (string.IsNullOrEmpty(endpoint))
                return "Unknown";

            var parts = endpoint.Split('/');
            if (parts.Length > 2 && parts[1].Equals("api", StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length > 3 && parts[2].Equals("v1", StringComparison.OrdinalIgnoreCase))
                    return parts.Length > 4 ? parts[3] : "Unknown";
                else
                    return parts[2];
            }

            return "Unknown";
        }
        
        /// <summary>
        /// API cevaplarının boyutunu sınırlandırır - Daha agresif sürüm
        /// </summary>
        /// <param name="details">API cevap detayları</param>
        /// <returns>Sınırlandırılmış detaylar</returns>
        private string LimitApiResponseSize(string details)
        {
            if (string.IsNullOrEmpty(details))
                return details;
                
            // Maksimum 1000 karakter ile sınırla (daha agresif küçültme)
            const int maxLength = 1000;
            const int maxFieldLength = 200; // Alanlar için daha küçük bir limit
            
            // Eğer zaten kısaysa, olduğu gibi döndür
            if (details.Length <= maxLength)
                return details;
                
            try
            {
                // Eğer JSON ise, içeriği akıllıca kısalt
                var jsonObj = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(details);
                var resultObj = new Dictionary<string, object>();
                
                // Tüm alanları kontrol et ve gerekirse kısalt
                foreach (var key in jsonObj.Keys)
                {
                    if (jsonObj[key] == null)
                    {
                        resultObj[key] = null;
                        continue;
                    }
                    
                    // Özel alanlar için daha agresif küçültme
                    if (key.Equals("response", StringComparison.OrdinalIgnoreCase) ||
                        key.Equals("data", StringComparison.OrdinalIgnoreCase) ||
                        key.Equals("result", StringComparison.OrdinalIgnoreCase) ||
                        key.Equals("items", StringComparison.OrdinalIgnoreCase) ||
                        key.Equals("records", StringComparison.OrdinalIgnoreCase))
                    {
                        string value = jsonObj[key].ToString();
                        if (value != null && value.Length > maxFieldLength)
                        {
                            // Sadece ilk maxFieldLength karakteri al
                            resultObj[key] = value.Substring(0, maxFieldLength) + "... [TRUNCATED]";
                        }
                        else
                        {
                            resultObj[key] = value;
                        }
                    }
                    // Diğer tüm alanlar için de boyut kontrolü yap
                    else
                    {
                        string value = jsonObj[key].ToString();
                        if (value != null && value.Length > maxFieldLength * 2) // Diğer alanlar için biraz daha fazla izin ver
                        {
                            resultObj[key] = value.Substring(0, maxFieldLength * 2) + "... [TRUNCATED]";
                        }
                        else
                        {
                            resultObj[key] = value;
                        }
                    }
                }
                
                // Eğer iç içe JSON nesneleri varsa, onları da işle
                TryProcessNestedJsonObjects(resultObj);
                
                // JSON'u tekrar serialize et
                string result = System.Text.Json.JsonSerializer.Serialize(resultObj);
                
                // Son bir kontrol - eğer hala çok büyükse, kesin olarak kısalt
                if (result.Length > maxLength)
                {
                    return result.Substring(0, maxLength) + "... [TRUNCATED]";
                }
                
                return result;
            }
            catch
            {
                // JSON değilse veya parse edilemezse, basitçe kısalt
                return details.Substring(0, Math.Min(maxLength, details.Length)) + (details.Length > maxLength ? "... [TRUNCATED]" : "");
            }
        }
        
        /// <summary>
        /// İç içe JSON nesnelerini işleyerek boyutlarını sınırlandırır
        /// </summary>
        private void TryProcessNestedJsonObjects(Dictionary<string, object> jsonObj)
        {
            const int maxNestedLength = 150; // İç içe nesneler için daha küçük limit
            
            foreach (var key in jsonObj.Keys.ToList())
            {
                if (jsonObj[key] == null) continue;
                
                string value = jsonObj[key].ToString();
                if (string.IsNullOrEmpty(value)) continue;
                
                // Eğer değer bir JSON nesnesiyse, onu da işle
                if ((value.StartsWith("{") && value.EndsWith("}")) ||
                    (value.StartsWith("[") && value.EndsWith("]")))
                {
                    try
                    {
                        // İç içe JSON'u parse et
                        var nestedObj = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(value);
                        
                        // İç içe nesnenin her bir alanını kontrol et ve gerekirse kısalt
                        foreach (var nestedKey in nestedObj.Keys.ToList())
                        {
                            if (nestedObj[nestedKey] == null) continue;
                            
                            string nestedValue = nestedObj[nestedKey].ToString();
                            if (nestedValue != null && nestedValue.Length > maxNestedLength)
                            {
                                nestedObj[nestedKey] = nestedValue.Substring(0, maxNestedLength) + "... [TRUNCATED]";
                            }
                        }
                        
                        // Kısaltılmış iç içe nesneyi ana nesneye geri yaz
                        jsonObj[key] = System.Text.Json.JsonSerializer.Serialize(nestedObj);
                    }
                    catch
                    {
                        // İç içe JSON parse edilemezse, değeri olduğu gibi bırak
                        // Ancak çok büyükse kısalt
                        if (value.Length > maxNestedLength * 2)
                        {
                            jsonObj[key] = value.Substring(0, maxNestedLength * 2) + "... [TRUNCATED]";
                        }
                    }
                }
            }
        }
    }
}
