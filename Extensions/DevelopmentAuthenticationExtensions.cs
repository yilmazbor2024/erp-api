using Microsoft.AspNetCore.Builder;
using ErpMobile.Api.Middleware;

namespace ErpMobile.Api.Extensions
{
    /// <summary>
    /// Development ortamında token doğrulamasını atlamak için extension methods
    /// </summary>
    public static class DevelopmentAuthenticationExtensions
    {
        /// <summary>
        /// Development ortamında token doğrulamasını atlayan middleware'i ekler
        /// </summary>
        public static IApplicationBuilder UseDevelopmentAuthentication(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DevelopmentAuthenticationMiddleware>();
        }
    }
}
