using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace ErpMobile.Api.Middleware
{
    /// <summary>
    /// Middleware to handle large request bodies by truncating them before they reach the controller
    /// </summary>
    public class RequestSizeLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestSizeLimitMiddleware> _logger;
        private readonly string[] _pathsToProcess = new[] { "/api/v1/auditlog/log-api-call" };
        private const int MaxRequestSize = 500 * 1024; // 500KB
        private const long KestrelMaxBodySize = 104857600; // 100MB - same as in Program.cs

        public RequestSizeLimitMiddleware(RequestDelegate next, ILogger<RequestSizeLimitMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // For audit log API calls, we need to increase the max request body size limit
            if (_pathsToProcess.Any(p => context.Request.Path.StartsWithSegments(p, StringComparison.OrdinalIgnoreCase)))
            {
                // Temporarily increase the request body limit for this specific request
                var originalBodyFeature = context.Features.Get<IHttpMaxRequestBodySizeFeature>();
                if (originalBodyFeature != null && originalBodyFeature.IsReadOnly == false)
                {
                    originalBodyFeature.MaxRequestBodySize = KestrelMaxBodySize;
                }
                
                // Check if this is a POST request with content that's still large
                if (context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase) && 
                    context.Request.ContentLength.HasValue && 
                    context.Request.ContentLength > MaxRequestSize)
                {
                    _logger.LogWarning($"Large request detected: {context.Request.Path}, size: {context.Request.ContentLength} bytes");
                    
                    try
                    {
                        // Save the original request body
                        var originalBody = context.Request.Body;
                        
                        // Enable buffering so we can read the stream multiple times
                        context.Request.EnableBuffering();
                        
                        // Read the request body
                        string requestBody;
                        using (var reader = new StreamReader(
                            context.Request.Body,
                            encoding: Encoding.UTF8,
                            detectEncodingFromByteOrderMarks: false,
                            leaveOpen: true))
                        {
                            // Read only up to a safe limit to prevent OOM
                            var buffer = new char[Math.Min(context.Request.ContentLength.Value, 10 * 1024 * 1024)]; // Max 10MB read
                            var bytesRead = await reader.ReadBlockAsync(buffer, 0, buffer.Length);
                            requestBody = new string(buffer, 0, bytesRead);
                            
                            // If we didn't read the entire body, append a note
                            if (bytesRead < context.Request.ContentLength.Value)
                            {
                                requestBody += "... [CONTENT TRUNCATED BY MIDDLEWARE]";
                            }
                            
                            context.Request.Body.Position = 0; // Reset the position
                        }

                        // Process the request body to reduce its size
                        string processedBody = TruncateRequestBody(requestBody);
                        
                        // Replace the request body with the processed one
                        var processedBodyBytes = Encoding.UTF8.GetBytes(processedBody);
                        var memoryStream = new MemoryStream(processedBodyBytes);
                        context.Request.Body = memoryStream;
                        context.Request.ContentLength = processedBodyBytes.Length;
                        
                        // Reset the stream position
                        memoryStream.Position = 0;
                        
                        _logger.LogInformation($"Request size reduced from {requestBody.Length} to {processedBody.Length} bytes");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing request body: {Message}", ex.Message);
                        
                        // If we can't process the request, return a 413 Payload Too Large response
                        context.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
                        await context.Response.WriteAsync("Request body is too large to process. Please reduce the size of your request.");
                        return; // Short-circuit the pipeline
                    }
                }
            }
            
            try
            {
                // Call the next middleware
                await _next(context);
            }
            catch (Microsoft.AspNetCore.Server.Kestrel.Core.BadHttpRequestException ex) when (ex.Message.Contains("Request body too large"))
            {
                // Catch and handle the specific Kestrel exception for request body too large
                _logger.LogWarning("Request body too large exception caught by middleware: {Message}", ex.Message);
                
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
                await context.Response.WriteAsync("Request body is too large to process. Please reduce the size of your request.");
            }
        }

        /// <summary>
        /// Truncates the request body to reduce its size
        /// </summary>
        private string TruncateRequestBody(string requestBody)
        {
            if (string.IsNullOrEmpty(requestBody))
                return requestBody;

            try
            {
                // Try to parse as JSON
                var jsonDocument = JsonDocument.Parse(requestBody);
                var jsonObj = JsonSerializer.Deserialize<Dictionary<string, object>>(requestBody);
                
                // Process the "details" field which is likely to be large
                if (jsonObj.ContainsKey("details") && jsonObj["details"] != null)
                {
                    string details = jsonObj["details"].ToString();
                    if (details.Length > 1000)
                    {
                        // Aggressively truncate the details
                        jsonObj["details"] = details.Substring(0, 1000) + "... [TRUNCATED BY MIDDLEWARE]";
                    }
                }
                
                // Process other potentially large fields
                foreach (var key in jsonObj.Keys.ToList())
                {
                    if (jsonObj[key] == null) continue;
                    
                    string value = jsonObj[key].ToString();
                    if (value.Length > 5000)
                    {
                        jsonObj[key] = value.Substring(0, 5000) + "... [TRUNCATED BY MIDDLEWARE]";
                    }
                }
                
                return JsonSerializer.Serialize(jsonObj);
            }
            catch
            {
                // If not valid JSON or any error occurs, truncate to a reasonable size
                const int maxSize = 500 * 1024; // 500KB
                if (requestBody.Length > maxSize)
                {
                    return requestBody.Substring(0, maxSize) + "... [TRUNCATED BY MIDDLEWARE]";
                }
                return requestBody;
            }
        }
    }
}
