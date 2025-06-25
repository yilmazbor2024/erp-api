using System.Text;
using System.Reflection;
using ErpMobile.Api.Data;
using ErpMobile.Api.Services.Auth;
using ErpMobile.Api.Services.Menu;
using ErpMobile.Api.Services;
using ErpMobile.Api.Services.ShipmentMethod;
using ErpMobile.Api.Services.Email;
using ErpApi.Services.Interfaces;
using ErpApi.Services;

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

using Serilog;

using Serilog.Events;

using Microsoft.AspNetCore.HttpOverrides;

using Microsoft.AspNetCore.Http;

using ErpMobile.Api.Extensions;

using ErpMobile.Api.Repositories.Product;

using ErpMobile.Api.Models.Product;

using ErpMobile.Api.Repositories.Inventory;

using ErpMobile.Api.Models.Inventory;

using ErpMobile.Api.Services.Interfaces;

using ErpMobile.Api.Repositories.CashAccount;

using ErpMobile.Api.Services.CashAccount;

using ErpMobile.Api.Repositories.CashTransaction;

using ErpMobile.Api.Services.CashTransaction;

using ErpMobile.Api.Middleware;

// Uygulama yapılandırmasını oluştur
var builder = WebApplication.CreateBuilder(args);

// IConfiguration için proxy oluştur
var originalConfiguration = builder.Configuration;
var configurationProxy = new ConnectionStringProxy(originalConfiguration);
builder.Services.AddSingleton<IConfiguration>(configurationProxy);

// Serilog yapılandırması

Log.Logger = new LoggerConfiguration()

.MinimumLevel.Debug()

.MinimumLevel.Override("Microsoft", LogEventLevel.Information)

.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)

.Enrich.FromLogContext()

.WriteTo.Console()

.WriteTo.File(

path: "logs/erp-api-.log",

rollingInterval: RollingInterval.Day,

retainedFileCountLimit: 31,

fileSizeLimitBytes: 10 * 1024 * 1024,

outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")

.CreateLogger();



// Serilog'u host builder'a ekle

builder.Host.UseSerilog();



// Core katmanındaki appsettings.json'ı ekle

builder.Configuration.SetBasePath(Path.GetDirectoryName(typeof(NanoServiceDbContext).Assembly.Location)!)

.AddJsonFile("appsettings.json", optional: false);

builder.Logging.SetMinimumLevel(LogLevel.Information);



// Add services to the container.

// CORS yapılandırması - tüm kaynaklara izin ver

builder.Services.AddCors(options =>

{

options.AddDefaultPolicy(policy =>

{

policy.SetIsOriginAllowed(_ => true) // Tüm kaynaklara izin ver

.AllowAnyHeader()

.AllowAnyMethod()

.AllowCredentials()

.WithExposedHeaders("Content-Disposition", "Content-Length");

});

});

builder.Services.AddControllers()

.AddJsonOptions(options => {

// JSON property adlarında camelCase kullanımını kabul et (JavaScript uyumluluğu için)

options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

})

.ConfigureApiBehaviorOptions(options => {

// Model doğrulama hatalarında otomatik BadRequest dönüşünü devre dışı bırak

options.SuppressModelStateInvalidFilter = true;

});

builder.Services.AddEndpointsApiExplorer();



// Swagger/OpenAPI configuration

builder.Services.AddSwaggerGen(c =>

{

c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {

Title = "ErpMobile API",

Version = "v1",

Description = "ErpMobile API for B2B operations",

Contact = new Microsoft.OpenApi.Models.OpenApiContact

{

Name = "Support Team",

Email = "info@edikravat.tr"

}

});



// Bearer token authentication için

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

new string[] {}

}

});


// XML belgeleri ekle (varsa)

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

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
builder.Services.AddDbContext<ErpDbContext>(options =>
{
    // appsettings.json'dan bağlantı dizesini al
    var connectionString = builder.Configuration.GetConnectionString("ErpConnection");
    
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ArgumentException("ErpConnection string is empty or not configured.");
    }
    
    Console.WriteLine($"ErpDbContext oluşturuluyor. Kullanılan bağlantı dizesi: {connectionString.Substring(0, Math.Min(20, connectionString.Length))}...");
    
    options.UseSqlServer(connectionString);
});

// SQL bağlantı fabrikasını kaydet
builder.Services.AddSingleton<SqlConnectionFactory>();



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




// ErpDbContext zaten yukarıda AddDbContext ile kaydedildi

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



// Serilog ile basit loglama yeterli



// Register Services

// Dapper için context ekleme

builder.Services.AddScoped<DapperContext>();

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

// Müşteri varsayılan adres servisi

builder.Services.AddScoped<ICustomerDefaultService, CustomerDefaultService>();

// Sevkiyat yöntemi servisi

builder.Services.AddScoped<IShipmentMethodService, ShipmentMethodService>();

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

builder.Services.AddScoped<ICustomerCreditService, CustomerCreditService>();



// Döviz kuru servisi

builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();

// Tüm gereksiz referanslar kaldırıldı çünkü ERP veritabanına yazma işlemi yapmıyoruz

builder.Services.AddHttpClient();



// Fatura servisleri

builder.Services.AddTransient<IInvoiceRepository, InvoiceRepository>();

builder.Services.AddTransient<IInvoiceService, InvoiceService>();

builder.Services.AddScoped<IPaymentService, PaymentService>();

// Vadeli ödeme servisi
builder.Services.AddScoped<ICreditPaymentService, CreditPaymentService>();

builder.Services.AddScoped<IInvoiceReportRepository, InvoiceReportRepository>();

builder.Services.AddScoped<IInvoiceReportService, InvoiceReportService>();



// Ürün servisleri

builder.Services.AddScoped<IProductRepository, ProductRepository>();



// Vergi servisleri

builder.Services.AddScoped<ITaxTypeService, TaxTypeService>();



// Envanter servisleri
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

// Depolar arası sevk servisleri
builder.Services.AddScoped<IWarehouseTransferRepository, WarehouseTransferRepository>();

// Geçici Müşteri Token Servisi

builder.Services.AddScoped<TempCustomerTokenService>();

builder.Services.AddScoped<ITokenValidationService, TempCustomerTokenService>();



// Token ile müşteri oluşturma servisi

builder.Services.AddScoped<ICustomerTokenService, CustomerTokenService>();



// DapperContext kaydı

builder.Services.AddSingleton<ErpMobile.Api.Data.DapperContext>();

// HttpContext'e erişim için
builder.Services.AddHttpContextAccessor();



// Kasa Hesapları Servisleri

builder.Services.AddScoped<ICashAccountRepository, CashAccountRepository>();

builder.Services.AddScoped<ICashAccountService, CashAccountService>();

builder.Services.AddScoped<ICashTransactionRepository, CashTransactionRepository>();

builder.Services.AddScoped<ICashTransactionService, CashTransactionService>();

builder.Services.AddScoped<ICashService, CashService>();



// Ödeme Servisi

builder.Services.AddScoped<IPaymentService, PaymentService>();



builder.Logging.ClearProviders();

builder.Logging.AddConsole();

builder.Logging.AddDebug();

builder.Logging.SetMinimumLevel(LogLevel.Information);



// HTTPS yapılandırması devre dışı bırakıldı

// IIS SSL sonlandırma işlemini kendisi yapacak

// builder.Services.AddHttpsRedirection(options =>

// {

// options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;

// options.HttpsPort = 443;

// });



// Forward Headers yapılandırması

builder.Services.Configure<ForwardedHeadersOptions>(options =>

{

options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

options.KnownNetworks.Clear();

options.KnownProxies.Clear();

});



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

// app zaten yukarıda tanımlanmış

// Middleware sıralaması önemli: önce ForwardedHeaders, sonra CORS, sonra diğerleri

// IIS arkasında çalışırken HTTPS yönlendirmesi için gerekli
app.UseForwardedHeaders();

// Hata yönetimi için Exception Handler middleware'i
app.UseExceptionHandler("/Error");

// Swagger'ı her ortamda etkinleştir
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ErpMobile API V1");
    c.RoutePrefix = "swagger";
});

// İstek günlükleme middleware'i
app.UseSerilogRequestLogging(options => {
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
});

// Statik dosyaları etkinleştir
app.UseStaticFiles();

// HTTPS yönlendirmesini devre dışı bırakıyoruz
// IIS SSL sonlandırma işlemini kendisi yapacak
// app.UseHttpsRedirection();

// Routing middleware'i - UseEndpoints'ten önce olmalı
app.UseRouting();

// Veritabanı seçim middleware'i - en erken çalışması için UseRouting'den hemen sonra yerleştiriyoruz
app.UseDatabaseSelection();

// CORS politikasını uygula - UseRouting ve UseEndpoints arasında olmalı
app.UseCors();

// Authentication ve Authorization middleware'leri - UseRouting ve UseEndpoints arasında olmalı
app.UseDevelopmentAuthentication();
app.UseAuthentication();
app.UseAuthorization();

// Endpoints middleware'i - en son çağrılmalı
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    
    // Kök dizine gelen istekler için basit bir yanıt
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("ERP API is running. Please use /api/v1/ endpoints.");
    });
    
    // Basit bir sağlık kontrolü endpoint'i
    endpoints.MapGet("/health", async context =>
    {
        context.Response.StatusCode = StatusCodes.Status200OK;
        await context.Response.WriteAsync("Healthy");
    });
});

// ErpConnectionStringProvider'a servis sağlayıcıyı ayarla
ErpConnectionStringProvider.SetServiceProvider(app.Services);

app.Run();