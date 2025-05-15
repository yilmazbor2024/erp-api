using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ErpMobile.Api.Middleware
{
    public class LegacyApiRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public LegacyApiRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;
            
            // Eski Customer endpoint'lerini yönlendir
            if (path.StartsWith("/api/Customer/") || path.StartsWith("/api/v1/Customer/"))
            {
                // Yeni endpoint'e yönlendir
                if (path.Contains("/credit-info"))
                {
                    var newPath = path.Replace("/api/Customer/", "/api/v1/CustomerFinancial/")
                                     .Replace("/api/v1/Customer/", "/api/v1/CustomerFinancial/");
                    context.Response.Redirect(newPath);
                    return;
                }
                else if (path.Contains("/transactions"))
                {
                    var newPath = path.Replace("/api/Customer/", "/api/v1/CustomerFinancial/")
                                     .Replace("/api/v1/Customer/", "/api/v1/CustomerFinancial/");
                    context.Response.Redirect(newPath);
                    return;
                }
                else if (path.Contains("/addresses") || path.Contains("/address-types"))
                {
                    var newPath = path.Replace("/api/Customer/", "/api/v1/CustomerAddress/")
                                     .Replace("/api/v1/Customer/", "/api/v1/CustomerAddress/");
                    context.Response.Redirect(newPath);
                    return;
                }
                else if (path.Contains("/communications"))
                {
                    var newPath = path.Replace("/api/Customer/", "/api/v1/CustomerCommunication/")
                                     .Replace("/api/v1/Customer/", "/api/v1/CustomerCommunication/");
                    context.Response.Redirect(newPath);
                    return;
                }
                else if (path.Contains("/contacts") || path.Contains("/contact-types"))
                {
                    var newPath = path.Replace("/api/Customer/", "/api/v1/CustomerContact/")
                                     .Replace("/api/v1/Customer/", "/api/v1/CustomerContact/");
                    context.Response.Redirect(newPath);
                    return;
                }
                else if (path.Contains("/regions") || path.Contains("/states") || 
                         path.Contains("/cities") || path.Contains("/districts"))
                {
                    var newPath = path.Replace("/api/Customer/", "/api/v1/CustomerLocation/")
                                     .Replace("/api/v1/Customer/", "/api/v1/CustomerLocation/");
                    context.Response.Redirect(newPath);
                    return;
                }
                // Müşteri oluşturma endpointlerini yönlendirme dışında tut
                else if (path.Contains("/create-basic") || path.Contains("/create-new"))
                {
                    // POST isteklerini yönlendirme
                    if (context.Request.Method == "POST")
                    {
                        // POST isteklerini yönlendirme, doğrudan CustomerController'a gitsin
                        await _next(context);
                        return;
                    }
                    else
                    {
                        // GET isteklerini yönlendir
                        var newPath = path.Replace("/api/Customer/", "/api/v1/CustomerBasic/")
                                         .Replace("/api/v1/Customer/", "/api/v1/CustomerBasic/");
                        context.Response.Redirect(newPath);
                        return;
                    }
                }
                else
                {
                    var newPath = path.Replace("/api/Customer/", "/api/v1/CustomerBasic/")
                                     .Replace("/api/v1/Customer/", "/api/v1/CustomerBasic/");
                    context.Response.Redirect(newPath);
                    return;
                }
            }

            await _next(context);
        }
    }
}
