using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ErpMobile.Api.Middleware
{
    /// <summary>
    /// Development ortamında token doğrulamasını atlayan middleware
    /// </summary>
    public class DevelopmentAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;

        public DevelopmentAuthenticationMiddleware(RequestDelegate next, IHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Development ortamında ve API isteği ise token kontrolünü atla
            if (_environment.IsDevelopment() && IsApiRequest(context) && !IsAuthRequest(context))
            {
                // Development ortamında test kullanıcısı bilgilerini ekle
                // Bu, token olmadan API'ye erişimi sağlar
                context.Items["SkipAuthentication"] = true;
            }

            await _next(context);
        }

        private bool IsApiRequest(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/api");
        }

        private bool IsAuthRequest(HttpContext context)
        {
            // Auth controller isteklerini atla, çünkü bunlar zaten [AllowAnonymous] ile işaretli
            return context.Request.Path.StartsWithSegments("/api/Auth") || 
                   context.Request.Path.StartsWithSegments("/api/v1/Auth");
        }
    }
}
