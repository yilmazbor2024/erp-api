using System.Text;
using ErpMobile.Api.Data;
using ErpMobile.Api.Services.Auth;
using ErpMobile.Api.Services.Menu;
using ErpMobile.Api.Services.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ErpMobile.Api.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ErpMobile.Api.Services;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Repositories.Invoice;
using ErpMobile.Api.Services.Invoice;
using ErpMobile.Api.Extensions;
using ErpMobile.Api.Repositories.Product;
using ErpMobile.Api.Models.Product;

var builder = WebApplication.CreateBuilder(args);

// Core katmanındaki appsettings.json'ı ekle
builder.Configuration.SetBasePath(Path.GetDirectoryName(typeof(NanoServiceDbContext).Assembly.Location)!)
    .AddJsonFile("appsettings.json", optional: false);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://192.168.1.113:3000", "http://192.168.1.113:3001")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => {
        // Model doğrulama hatalarında otomatik BadRequest dönüşünü devre dışı bırak
        options.SuppressModelStateInvalidFilter = true;
    });
builder.Services.AddEndpointsApiExplorer();

// Swagger/OpenAPI configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "ERP Mobile API", 
        Version = "v1",
        Description = "ERP Mobile uygulaması için REST API",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "ERP Mobile Team",
            Email = "support@erp-mobile.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // XML Belgeleme Dosyasını Yükle
    var xmlFile = "ErpMobile.Api.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
    
    // Şema çakışmalarını önlemek için benzersiz şema ID'leri oluştur
    c.CustomSchemaIds(type => type.FullName);
    
    // Rota çakışmalarını önlemek için
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

// Bağlantı dizelerini try bloğunun dışında tanımla
var nanoServiceConnectionString = builder.Configuration.GetConnectionString("NanoServiceConnection");
var erpConnectionString = builder.Configuration.GetConnectionString("ErpConnection");

Console.WriteLine($"NanoServiceConnection: {nanoServiceConnectionString?.Substring(0, Math.Min(20, nanoServiceConnectionString?.Length ?? 0))}...");
Console.WriteLine($"ErpConnection: {erpConnectionString?.Substring(0, Math.Min(20, erpConnectionString?.Length ?? 0))}...");

try
{
    if (string.IsNullOrEmpty(nanoServiceConnectionString))
    {
        throw new ArgumentException("NanoServiceConnection string is empty or not configured.");
    }

    // Configure NanoServiceDb context
    builder.Services.AddDbContext<NanoServiceDbContext>(options =>
        options.UseSqlServer(nanoServiceConnectionString));

    // Register ErpDbContext
    builder.Services.AddScoped<ErpDbContext>(provider => 
        new ErpDbContext(
            erpConnectionString ?? throw new ArgumentException("ErpConnection string is empty or not configured."), 
            provider.GetRequiredService<ILogger<ErpDbContext>>()
        ));

    Console.WriteLine("Database contexts configured with SQL Server.");
}
catch (Exception ex)
{
    // Log the database connection error
    Console.WriteLine($"SQL Server connection error: {ex.Message}");
    Console.WriteLine("Using SQL Server connection anyway, application might fail if connection is not available.");
    
    // Register SQL Server DbContext even if connection test fails
    builder.Services.AddDbContext<NanoServiceDbContext>(options => 
        options.UseSqlServer(nanoServiceConnectionString));
        
    builder.Services.AddScoped<ErpDbContext>(provider => 
        new ErpDbContext(
            erpConnectionString ?? throw new ArgumentException("ErpConnection string is empty or not configured."), 
            provider.GetRequiredService<ILogger<ErpDbContext>>()
        ));
}

// Configure Identity
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    
    options.User.RequireUniqueEmail = true;
    
    // Email onayı devre dışı bırakıldı
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<NanoServiceDbContext>()
.AddDefaultTokenProviders();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is missing")))
    };
});
// Normal JWT doğrulaması kullanılıyor

// Register Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IJwtService, JwtService>();
// Müşteri servisleri
builder.Services.AddScoped<ICustomerService>(provider => new CustomerBasicService(
    provider.GetRequiredService<ILogger<CustomerBasicService>>(),
    provider.GetRequiredService<IConfiguration>(),
    provider.GetRequiredService<ILoggerFactory>()
));
builder.Services.AddScoped<ICustomerServiceNew, CustomerServiceNew>();
builder.Services.AddScoped<CustomerStubService>();

// Depo ve vergi dairesi servisleri
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
// Ülke servisi
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICustomerAddressService, CustomerAddressService>();
builder.Services.AddScoped<CustomerAddressService>();
builder.Services.AddScoped<ICustomerCommunicationService, CustomerCommunicationService>();
builder.Services.AddScoped<ICustomerContactService, CustomerContactService>();
builder.Services.AddScoped<CustomerContactService>();
builder.Services.AddScoped<ICustomerFinancialDetailService, CustomerFinancialDetailService>();
builder.Services.AddScoped<ICustomerLocationService, CustomerLocationService>();
builder.Services.AddScoped<ICustomerFinancialService, CustomerFinancialService>();

// Bölge servisi
builder.Services.AddScoped<IStateService, StateService>();

// Lokasyon hiyerarşi servisi
builder.Services.AddScoped<ILocationService, LocationService>();

// Diğer servisler
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ICashService, CashService>();
builder.Services.AddScoped<ICustomerDebtService, CustomerDebtService>();
builder.Services.AddScoped<ICustomerCreditService, CustomerCreditService>();

// Fatura servisleri
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

// Ürün servisleri
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

try
{
    // Create default roles and admin user
    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            // Create roles if they don't exist
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    roleManager.CreateAsync(new Role 
                    { 
                        Name = roleName, 
                        Description = roleName + " role", 
                        IsActive = true, 
                        CreatedAt = DateTime.UtcNow, 
                        CreatedBy = "System" 
                    }).Wait();
                }
            }

            // Create admin user if it doesn't exist
            var adminEmail = "f1@nanobil.com.tr";
            var adminUser = userManager.FindByEmailAsync(adminEmail).Result;

            if (adminUser == null)
            {
                var admin = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    UserCode = "ADMIN001",
                    FirstName = "System",
                    LastName = "Administrator",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                };

                var result = userManager.CreateAsync(admin, "!Zsa2019!").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, "Admin").Wait();
                }
            }
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Roles ve Admin kullanıcı oluşturulurken hata meydana geldi.");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Roller ve admin kullanıcı oluşturulurken hata: {ex.Message}");
}

// Configure the HTTP request pipeline.
app.UseSwagger(c => {
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
    // Swagger JSON'ın boş olması durumunu engelle
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) => {
        if (!swaggerDoc.Paths.Any())
        {
            // OpenApiPaths oluştur (Dictionary değil)
            var paths = new Microsoft.OpenApi.Models.OpenApiPaths();
            
            // Customer path ekle
            paths.Add("/api/v1/Customer/customers", new Microsoft.OpenApi.Models.OpenApiPathItem
            {
                Operations = new Dictionary<Microsoft.OpenApi.Models.OperationType, Microsoft.OpenApi.Models.OpenApiOperation>
                {
                    {
                        Microsoft.OpenApi.Models.OperationType.Get,
                        new Microsoft.OpenApi.Models.OpenApiOperation
                        {
                            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new Microsoft.OpenApi.Models.OpenApiTag { Name = "Customer" } },
                            Summary = "Get customers",
                            Description = "Returns a list of customers",
                            Responses = new Microsoft.OpenApi.Models.OpenApiResponses()
                        }
                    }
                }
            });
            
            // Path'i swaggerDoc'a ata
            swaggerDoc.Paths = paths;
        }
    });
});

// Statik dosyaları etkinleştir
app.UseStaticFiles();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP Mobile API v1");
    c.RoutePrefix = "swagger";
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
    c.DefaultModelsExpandDepth(-1); // Modelleri gizle
    c.DisplayRequestDuration(); // İstek süresini göster
    
    // Swagger UI'ın hata durumunda da çalışmasını sağla
    c.ConfigObject.AdditionalItems["syntaxHighlight"] = false;
    c.ConfigObject.AdditionalItems["tryItOutEnabled"] = true;
    c.ConfigObject.AdditionalItems["persistAuthorization"] = true;
    c.ConfigObject.AdditionalItems["filter"] = "";
    c.InjectStylesheet("/swagger-ui/custom.css");
    
    // Özel indeks sayfası kullan
    var customIndexHtmlPath = Path.Combine(builder.Environment.WebRootPath, "swagger-ui", "index.html");
    if (File.Exists(customIndexHtmlPath))
    {
        c.IndexStream = () => File.OpenRead(customIndexHtmlPath);
    }
    else 
    {
        c.IndexStream = () => {
            var type = typeof(Program).Assembly.GetType("Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware");
            var assembly = type!.Assembly;
            var resourceStream = assembly.GetManifestResourceStream("Swashbuckle.AspNetCore.SwaggerUI.index.html");
            return resourceStream!;
        };
    }
});

app.UseHttpsRedirection();
app.UseRouting();

// CORS middleware'i UseRouting ve UseAuthentication arasında olmalı
app.UseCors(builder =>
    builder.WithOrigins("http://localhost:3000", "http://192.168.1.113:3000")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials());

// Eski API endpoint'lerini yeni endpoint'lere yönlendir
// Middleware devre dışı bırakıldı
// app.UseMiddleware<ErpMobile.Api.Middleware.LegacyApiRedirectMiddleware>();

// Development ortamında token doğrulamasını atlayan middleware'i ekle
app.UseDevelopmentAuthentication();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
