using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Customer;
using ErpMobile.Api.Models.Contact;
using ErpMobile.Api.Services;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.Data.SqlClient;
using ErpMobile.Api.Data;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Common;
using Microsoft.AspNetCore.Http;
using Dapper;
using Microsoft.Extensions.Configuration;
using ErpMobile.Api.Models.Requests; 
using ErpMobile.Api.Models.Responses; 
using ErpMobile.Api.Services.Interfaces; // Eklendi
using ErpMobile.Api.Interfaces; // ICustomerTokenService için eklendi

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        private readonly ErpDbContext _context;
        private readonly ICustomerServiceNew _customerServiceNew;
        private readonly IConfiguration _configuration;
        private readonly CustomerStubService _customerStubService;
        private readonly TempCustomerTokenService _tempCustomerTokenService;
        private readonly ITokenValidationService _tokenValidationService;
        private readonly ICustomerTokenService _customerTokenService;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger, ErpDbContext context, ICustomerServiceNew customerServiceNew, IConfiguration configuration, CustomerStubService customerStubService, TempCustomerTokenService tempCustomerTokenService, ITokenValidationService tokenValidationService, ICustomerTokenService? customerTokenService = null)
        {
            _customerService = customerService;
            _logger = logger;
            _context = context;
            _customerServiceNew = customerServiceNew;
            _configuration = configuration;
            _customerStubService = customerStubService;
            _tempCustomerTokenService = tempCustomerTokenService;
            _tokenValidationService = tokenValidationService;
            _customerTokenService = customerTokenService;
        }

        /// <summary>
        /// Get customers
        /// </summary>
        /// <param name="filter">Filter parameters for customers</param>
        /// <returns>List of customers</returns>
        [HttpGet("customers")]
        [ProducesResponseType(typeof(ApiResponse<PagedResponse<CustomerListResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomers([FromQuery] CustomerFilterRequest filter)
        {
            try
            {
                // Initialize filter if null
                filter ??= new CustomerFilterRequest();
                
                // Set default values if not specified
                if (filter.PageNumber <= 0) filter.PageNumber = 1;
                if (filter.PageSize <= 0) filter.PageSize = 20;
                
                var customers = await _customerService.GetCustomerListAsync(filter);
                return Ok(new ApiResponse<PagedResponse<CustomerListResponse>>(customers, true, "Customers retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customers. Filter: {@Filter}", filter);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(null, false, "An error occurred while getting customers.", ex.Message));
            }
        }

        /// <summary>
        /// Müşteri detayını getirir
        /// </summary>
        [HttpGet("{customerCode}")]
        public async Task<ActionResult<CustomerDetailResponse>> GetCustomerByCode(string customerCode)
        {
            try
            {
                var customer = await _customerService.GetCustomerByCodeAsync(customerCode);
                if (customer == null)
                {
                    return NotFound($"Customer not found with code: {customerCode}");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer details. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="request">Customer create request</param>
        /// <returns>Created customer</returns>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateRequest request)
        {
            try
            {

                // Müşteri oluştur
                var customer = await _customerService.CreateCustomerAsync(request);

                // Normal müşteri oluşturma işlemi - token kullanımı kaldırıldı

                var response = new ApiResponse<CustomerResponse>
                {
                    Success = true,
                    Data = customer,
                    Message = "Customer created successfully"
                };
                return CreatedAtAction(nameof(GetCustomerByCode), new { customerCode = customer.CustomerCode }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer");
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Data = null,
                    Message = "An error occurred while creating the customer",
                    Error = ex.Message
                };
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Müşteri kodu ile token alma
        /// </summary>
        /// <param name="request">Müşteri kodu içeren istek</param>
        /// <returns>Müşteri için token</returns>
        [HttpPost("get-token")]
        [ProducesResponseType(typeof(ApiResponse<TokenResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerToken([FromBody] CustomerTokenRequest request)
        {
            try
            {
                _logger.LogInformation($"[CustomerController.GetCustomerToken] - Müşteri için token istendi: {request?.CustomerCode}");
                
                if (string.IsNullOrEmpty(request?.CustomerCode))
                {
                    return BadRequest(new ApiResponse<string>(null, false, "Müşteri kodu gereklidir."));
                }
                
                // Müşteri kodunun geçerliliğini kontrol et
                var customer = await _customerService.GetCustomerByCodeAsync(request.CustomerCode);
                if (customer == null)
                {
                    return BadRequest(new ApiResponse<string>(null, false, $"Müşteri bulunamadı: {request.CustomerCode}"));
                }
                
                // Müşteri için token oluştur
                var token = await _tokenValidationService.GenerateTokenAsync(request.CustomerCode);
                
                _logger.LogInformation($"[CustomerController.GetCustomerToken] - Müşteri için token oluşturuldu: {request.CustomerCode}");
                
                return Ok(new ApiResponse<TokenResponse>(
                    new TokenResponse { Token = token },
                    true,
                    "Müşteri için token başarıyla oluşturuldu."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerController.GetCustomerToken] - Müşteri için token oluşturulurken hata: {CustomerCode}", request?.CustomerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new ApiResponse<string>(null, false, "Müşteri için token oluşturulurken bir hata oluştu.", ex.Message));
            }
        }
        
        /// <summary>
        /// Token ile yeni müşteri oluşturma (müşteri kayıt formu için)
        /// </summary>
        /// <param name="token">Doğrulama token'ı</param>
        /// <param name="request">Müşteri oluşturma isteği</param>
        /// <returns>Oluşturulan müşteri</returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(typeof(ApiResponse<CustomerResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse<CustomerResponse>), 400)]
        [ProducesResponseType(typeof(ApiResponse<CustomerResponse>), 401)]
        [ProducesResponseType(typeof(ApiResponse<CustomerResponse>), 500)]
        public async Task<IActionResult> RegisterCustomer([FromQuery] string token, [FromBody] JsonElement requestDataElement)
        {
            try
            {
                // Gelen veriyi logla
                _logger.LogInformation("RegisterCustomer: Received request data");
                
                // JsonElement'i CustomerCreateRequestNew modeline dönüştür
                CustomerCreateRequestNew request = new CustomerCreateRequestNew();
                
                try
                {
                    // CustomerCode
                    if (requestDataElement.TryGetProperty("customerCode", out JsonElement customerCodeElement) && customerCodeElement.ValueKind != JsonValueKind.Null)
                        request.CustomerCode = customerCodeElement.GetString() ?? string.Empty;
                    else
                        request.CustomerCode = string.Empty;
                        
                    // CustomerName
                    if (requestDataElement.TryGetProperty("customerName", out JsonElement customerNameElement) && customerNameElement.ValueKind != JsonValueKind.Null)
                        request.CustomerName = customerNameElement.GetString() ?? string.Empty;
                    else
                        request.CustomerName = string.Empty;
                        
                    // CustomerSurname
                    if (requestDataElement.TryGetProperty("customerSurname", out JsonElement customerSurnameElement) && customerSurnameElement.ValueKind != JsonValueKind.Null)
                        request.CustomerSurname = customerSurnameElement.GetString() ?? string.Empty;
                    else
                        request.CustomerSurname = string.Empty;
                        
                    // CustomerTypeCode
                    if (requestDataElement.TryGetProperty("customerTypeCode", out JsonElement customerTypeCodeElement) && customerTypeCodeElement.ValueKind != JsonValueKind.Null)
                    {
                        if (customerTypeCodeElement.ValueKind == JsonValueKind.String && customerTypeCodeElement.GetString() == "individual")
                            request.CustomerTypeCode = 3; // Bireysel müşteri için varsayılan değer
                        else if (customerTypeCodeElement.ValueKind == JsonValueKind.Number)
                            request.CustomerTypeCode = customerTypeCodeElement.GetByte();
                        else
                            request.CustomerTypeCode = 3; // Varsayılan değer
                    }
                    else
                        request.CustomerTypeCode = 3;
                        
                    // CompanyCode
                    if (requestDataElement.TryGetProperty("companyCode", out JsonElement companyCodeElement) && companyCodeElement.ValueKind != JsonValueKind.Null)
                    {
                        if (companyCodeElement.ValueKind == JsonValueKind.Number)
                            request.CompanyCode = companyCodeElement.GetInt16();
                        else
                            request.CompanyCode = 1;
                    }
                    else
                        request.CompanyCode = 1;
                        
                    // OfficeCode
                    if (requestDataElement.TryGetProperty("officeCode", out JsonElement officeCodeElement) && officeCodeElement.ValueKind != JsonValueKind.Null)
                        request.OfficeCode = officeCodeElement.GetString() ?? "M";
                    else
                        request.OfficeCode = "M";
                        
                    // CurrencyCode
                    if (requestDataElement.TryGetProperty("currencyCode", out JsonElement currencyCodeElement) && currencyCodeElement.ValueKind != JsonValueKind.Null)
                        request.CurrencyCode = currencyCodeElement.GetString() ?? "USD";
                    else
                        request.CurrencyCode = "USD";
                        
                    // IsIndividualAcc
                    if (requestDataElement.TryGetProperty("isIndividualAcc", out JsonElement isIndividualAccElement) && isIndividualAccElement.ValueKind != JsonValueKind.Null)
                        request.IsIndividualAcc = isIndividualAccElement.GetBoolean();
                    else
                        request.IsIndividualAcc = true;
                        
                    // TaxNumber
                    if (requestDataElement.TryGetProperty("taxNumber", out JsonElement taxNumberElement) && taxNumberElement.ValueKind != JsonValueKind.Null)
                        request.TaxNumber = taxNumberElement.GetString() ?? string.Empty;
                    else
                        request.TaxNumber = string.Empty;
                        
                    // TaxOfficeCode
                    if (requestDataElement.TryGetProperty("taxOfficeCode", out JsonElement taxOfficeCodeElement) && taxOfficeCodeElement.ValueKind != JsonValueKind.Null)
                        request.TaxOfficeCode = taxOfficeCodeElement.GetString() ?? string.Empty;
                    else
                        request.TaxOfficeCode = string.Empty;
                        
                    // IdentityNum
                    if (requestDataElement.TryGetProperty("identityNum", out JsonElement identityNumElement) && identityNumElement.ValueKind != JsonValueKind.Null)
                        request.IdentityNum = identityNumElement.GetString() ?? string.Empty;
                    else
                        request.IdentityNum = string.Empty;
                        
                    // CreatedUserName
                    if (requestDataElement.TryGetProperty("createdUserName", out JsonElement createdUserNameElement) && createdUserNameElement.ValueKind != JsonValueKind.Null)
                        request.CreatedUserName = createdUserNameElement.GetString() ?? "system";
                    else
                        request.CreatedUserName = "system";
                        
                    // LastUpdatedUserName
                    if (requestDataElement.TryGetProperty("lastUpdatedUserName", out JsonElement lastUpdatedUserNameElement) && lastUpdatedUserNameElement.ValueKind != JsonValueKind.Null)
                        request.LastUpdatedUserName = lastUpdatedUserNameElement.GetString() ?? "system";
                    else
                        request.LastUpdatedUserName = "system";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "RegisterCustomer: Error parsing request data");
                    return BadRequest(new ApiResponse<CustomerResponse>
                    {
                        Success = false,
                        Message = "Müşteri bilgileri geçersiz",
                        Data = null,
                        Error = ex.Message
                    });
                }
                
                // Adres ve iletişim bilgilerini ayrıca işleyeceğiz
                // Not: Bu bilgiler müşteri oluştuktan sonra ayrı API çağrıları ile eklenecek
                
                // Model binding hatalarını kontrol et
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new { Property = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToList() })
                        .ToList();
                    
                    _logger.LogError("RegisterCustomer: Model binding errors: {@Errors}", errors);
                    
                    return BadRequest(new ApiResponse<CustomerResponse>
                    {
                        Success = false,
                        Message = "Müşteri bilgileri geçersiz",
                        Data = null,
                        Error = string.Join(", ", errors.SelectMany(e => e.Errors))
                    });
                }

                // Token geçerliliğini kontrol et
                var tokenValidation = await _tokenValidationService.ValidateTokenAsync(token, TokenScope.CustomerRegistration);
                if (!tokenValidation.IsValid)
                {
                    _logger.LogWarning("Token validation failed: {reason}", tokenValidation.Message);
                    return Unauthorized(new ApiResponse<CustomerResponse>
                    {
                        Success = false,
                        Message = "Geçersiz token",
                        Data = null,
                        Error = tokenValidation.Message
                    });
                }

                _logger.LogInformation("RegisterCustomer: Token validated successfully for customer: {CustomerName}", request?.CustomerName ?? "Unknown");

                // Varsayılan değerleri ayarla
                request.CreatedUserName = "system";
                request.LastUpdatedUserName = "system";
                
                // Varsayılan değerler
                if (request.CompanyCode <= 0) request.CompanyCode = 1;
                if (string.IsNullOrEmpty(request.OfficeCode)) request.OfficeCode = "M";
                if (string.IsNullOrEmpty(request.CurrencyCode)) request.CurrencyCode = "USD";
                
                // Müşteri tipi varsayılan olarak 3 (normal müşteri)
                if (request.CustomerTypeCode <= 0) request.CustomerTypeCode = 3;
                
                // Müşteri oluşturma işlemi
                CustomerCreateResponse result;
                if (_customerTokenService != null)
                {
                    // Token servisi varsa, token servisi ile müşteri oluştur
                    _logger.LogInformation("Token servisi ile müşteri oluşturuluyor");
                    result = await _customerTokenService.CreateCustomerWithTokenAsync(request);
                }
                else
                {
                    // Token servisi yoksa, normal servis ile müşteri oluştur
                    _logger.LogInformation("Normal servis ile müşteri oluşturuluyor");
                    var resultNew = await _customerServiceNew.CreateCustomerAsync(request);
                    
                    // CustomerCreateResponseNew'i CustomerCreateResponse'a dönüştür
                    result = new CustomerCreateResponse
                    {
                        CustomerCode = resultNew.CustomerCode,
                        CustomerName = resultNew.CustomerName,
                        TaxNumber = resultNew.TaxNumber,
                        TaxOfficeCode = resultNew.TaxOfficeCode,
                        CustomerTypeCode = resultNew.CustomerTypeCode,
                        CustomerTypeDescription = resultNew.CustomerTypeDescription,
                        Success = resultNew.Success,
                        Message = resultNew.Message,
                        CreatedDate = resultNew.CreatedDate,
                        CreatedBy = resultNew.CreatedUsername
                    };
                }

                // CustomerCreateResponse sınıfında Success ve ErrorMessage özellikleri yok
                // Bu nedenle her zaman başarılı kabul ediyoruz
                if (string.IsNullOrEmpty(result.CustomerCode))
                {
                    _logger.LogError("Customer creation failed: No customer code returned");
                    return StatusCode(500, new ApiResponse<CustomerResponse>
                    {
                        Success = false,
                        Message = "Müşteri oluşturma başarısız",
                        Data = null,
                        Error = "Müşteri kodu oluşturulamadı"
                    });
                }

                // CustomerResponse nesnesini oluştur
                var customer = new CustomerResponse
                {
                    Success = true,
                    CustomerCode = result.CustomerCode,
                    CustomerName = result.CustomerName,
                    TaxNumber = request.TaxNumber ?? string.Empty,
                    TaxOfficeCode = request.TaxOfficeCode ?? string.Empty,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                };

                var response = new ApiResponse<CustomerResponse>
                {
                    Success = true,
                    Data = customer,
                    Message = "Müşteri başarıyla oluşturuldu"
                };
                
                _logger.LogInformation("Customer created successfully with code: {code}", result.CustomerCode);
                
                // Token'ı yanıta ekle
                Response.Headers.Add("X-Customer-Token", token);
                
                // Müşteri kodunu doğru şekilde döndür
                var directResponse = new
                {
                    success = true,
                    message = "Müşteri başarıyla oluşturuldu",
                    customerCode = result.CustomerCode,
                    customerName = result.CustomerName,
                    taxNumber = result.TaxNumber,
                    taxOfficeCode = result.TaxOfficeCode,
                    email = request.Email,
                    phoneNumber = request.PhoneNumber
                };
                
                // Yanıtı döndür
                return Ok(directResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RegisterCustomer: Error creating customer with token");
                return StatusCode(500, new ApiResponse<CustomerResponse>
                {
                    Success = false,
                    Message = "Müşteri oluşturulurken hata oluştu",
                    Data = null,
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Müşteri adreslerini getirir
        /// </summary>
        [HttpGet("{customerCode}/addresses")]
        public async Task<ActionResult<List<CustomerAddressResponse>>> GetCustomerAddresses(string customerCode)
        {
            try
            {
                var addresses = await _customerStubService.GetCustomerAddressesAsync(customerCode);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer addresses. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Müşteri iletişim bilgilerini getirir
        /// </summary>
        [HttpGet("{customerCode}/communications")]
        public async Task<ActionResult<List<CustomerCommunicationResponse>>> GetCustomerCommunications(string customerCode)
        {
            try
            {
                var communications = await _customerStubService.GetCustomerCommunicationsAsync(customerCode);
                return Ok(communications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer communications. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Müşteri kişilerini getirir
        /// </summary>
        [HttpGet("{customerCode}/contacts")]
        public async Task<ActionResult<List<CustomerContactResponse>>> GetCustomerContacts(string customerCode)
        {
            try
            {
                var contacts = await _customerStubService.GetContactsAsync(customerCode);
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer contacts. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Müşteri tiplerini getirir
        /// </summary>
        [HttpGet("types")]
        public async Task<ActionResult<List<CustomerTypeResponse>>> GetCustomerTypes()
        {
            try
            {
                var types = await _customerService.GetCustomerTypesAsync();
                return Ok(types);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri tipleri alınırken hata oluştu");
                return StatusCode(500, "Müşteri tipleri alınırken bir hata oluştu");
            }
        }

        /// <summary>
        /// Müşteri indirim gruplarını getirir
        /// </summary>
        [HttpGet("discount-groups")]
        public async Task<ActionResult<List<CustomerDiscountGroupResponse>>> GetCustomerDiscountGroups()
        {
            try
            {
                var groups = await _customerService.GetCustomerDiscountGroupsAsync();
                return Ok(groups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri indirim grupları alınırken hata oluştu");
                return StatusCode(500, "Müşteri indirim grupları alınırken bir hata oluştu");
            }
        }

        /// <summary>
        /// Müşteri ödeme planı gruplarını getirir
        /// </summary>
        [HttpGet("payment-plan-groups")]
        public async Task<ActionResult<List<CustomerPaymentPlanGroupResponse>>> GetCustomerPaymentPlanGroups()
        {
            try
            {
                var groups = await _customerService.GetCustomerPaymentPlanGroupsAsync();
                return Ok(groups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri ödeme planı grupları alınırken hata oluştu");
                return StatusCode(500, "Müşteri ödeme planı grupları alınırken bir hata oluştu");
            }
        }

        /// <summary>
        /// Bölgeleri getirir
        /// </summary>
        [HttpGet("regions")]
        public async Task<ActionResult<List<RegionResponse>>> GetRegions()
        {
            try
            {
                var regions = await _customerService.GetRegionsAsync();
                return Ok(regions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bölgeler alınırken hata oluştu");
                return StatusCode(500, "Bölgeler alınırken bir hata oluştu");
            }
        }

        /// <summary>
        /// İlleri getirir (States)
        /// </summary>
        [HttpGet("states")]
        public async Task<ActionResult<List<StateResponse>>> GetStates([FromQuery] string countryCode = null)
        {
            try
            {
                var states = await _customerService.GetStatesAsync(countryCode);
                return Ok(states);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İller alınırken hata oluştu");
                return StatusCode(500, "İller alınırken bir hata oluştu");
            }
        }

        /// <summary>
        /// Şehirleri getirir (Cities)
        /// </summary>
        [HttpGet("cities")]
        public async Task<ActionResult<List<CityResponse>>> GetCities()
        {
            try
            {
                var cities = await _customerService.GetCitiesAsync();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şehirler alınırken hata oluştu");
                return StatusCode(500, "Şehirler alınırken bir hata oluştu");
            }
        }

        /// <summary>
        /// İle göre şehirleri getirir
        /// </summary>
        [HttpGet("states/{stateCode}/cities")]
        public async Task<ActionResult<List<CityResponse>>> GetCitiesByState(string stateCode)
        {
            try
            {
                var cities = await _customerService.GetCitiesByStateAsync(stateCode);
                return Ok(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İle göre şehirler alınırken hata oluştu. İl Kodu: {StateCode}", stateCode);
                return StatusCode(500, "İle göre şehirler alınırken bir hata oluştu");
            }
        }

        /// <summary>
        /// Bölgeye göre şehirleri getirir
        /// </summary>
        [HttpGet("regions/{regionCode}/cities")]
        public async Task<ActionResult<List<CityResponse>>> GetCitiesByRegion(string regionCode)
        {
            try
            {
                var cities = await _customerService.GetCitiesByRegionAsync(regionCode);
                return Ok(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bölgeye göre şehirler alınırken hata oluştu. Bölge Kodu: {RegionCode}", regionCode);
                return StatusCode(500, "Bölgeye göre şehirler alınırken bir hata oluştu");
            }
        }

        /// <summary>
        /// Şehre göre ilçeleri getirir
        /// </summary>
        [HttpGet("cities/{cityCode}/districts")]
        public async Task<ActionResult<List<DistrictResponse>>> GetDistrictsByCity(string cityCode)
        {
            try
            {
                var districts = await _customerService.GetDistrictsByCityAsync(cityCode);
                return Ok(districts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şehre göre ilçeler alınırken hata oluştu. Şehir Kodu: {CityCode}", cityCode);
                return StatusCode(500, "Şehre göre ilçeler alınırken bir hata oluştu");
            }
        }

        /// <summary>
        /// İlçeleri getirir (Districts)
        /// </summary>
        [HttpGet("districts")]
        public async Task<ActionResult<List<DistrictResponse>>> GetDistricts()
        {
            try
            {
                var districts = await _customerService.GetAllDistrictsAsync();
                return Ok(districts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İlçeler alınırken hata oluştu");
                return StatusCode(500, "İlçeler alınırken bir hata oluştu");
            }
        }

        [HttpGet("address-types")]
        public async Task<ActionResult<List<AddressTypeResponse>>> GetAddressTypes()
        {
            try
            {
                var response = await _customerStubService.GetAddressTypesAsync();
                return Ok(response);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database connection error while getting address types");
                return StatusCode(500, "Veritabanı bağlantı hatası: Adres tipleri alınamadı. Lütfen sistem yöneticisiyle iletişime geçin.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting address types");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("address-types/{code}")]
        public async Task<ActionResult<AddressTypeResponse>> GetAddressTypeByCode(string code)
        {
            try
            {
                var response = await _customerStubService.GetAddressTypeByCodeAsync(code);
                if (response == null)
                {
                    return NotFound($"Address type not found with code: {code}");
                }
                return Ok(response);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database connection error while getting address type by code: {Code}", code);
                return StatusCode(500, "Veritabanı bağlantı hatası: Adres tipi alınamadı. Lütfen sistem yöneticisiyle iletişime geçin.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting address type by code");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("address-types")]
        public async Task<ActionResult<AddressTypeResponse>> CreateAddressType([FromBody] AddressTypeCreateRequest request)
        {
            try
            {
                // ERP sisteminde AddressType bir tablo değil, fonksiyon olduğu için ekleme yapılamaz
                return StatusCode(StatusCodes.Status405MethodNotAllowed, "ERP sisteminde AddressType bir fonksiyon/saklı prosedür olduğundan API üzerinden ekleme yapılamaz. Lütfen sistem yöneticinize başvurun.");
            }
            catch (NotSupportedException ex)
            {
                _logger.LogWarning(ex, "Operation not supported: Creating address type is not supported in ERP system");
                return StatusCode(StatusCodes.Status405MethodNotAllowed, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating address type");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("customers/{customerCode}/addresses")]
        public async Task<ActionResult<List<AddressResponse>>> GetAddresses(string customerCode)
        {
            try
            {
                var response = await _customerStubService.GetAddressesAsync(customerCode);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting addresses");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("customers/{customerCode}/addresses/{addressId}")]
        public async Task<ActionResult<AddressResponse>> GetAddressById(string customerCode, string addressId)
        {
            try
            {
                var response = await _customerStubService.GetAddressByIdAsync(customerCode, addressId);
                if (response == null)
                {
                    return NotFound($"Address not found for customer {customerCode} with id: {addressId}");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting address by id");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{customerCode}/addresses")]
        [ProducesResponseType(typeof(ApiResponse<AddressResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAddress(string customerCode, [FromBody] ErpMobile.Api.Models.Customer.AddressCreateRequest request)
        {
            try
            {
                // Validate request
                if (request == null)
                {
                    _logger.LogWarning("Address create request was null");
                    return BadRequest(new ApiResponse<string>("Address create request cannot be null", false, "Adres oluşturma isteği boş olamaz"));
                }

                var address = await _customerStubService.CreateAddressAsync(customerCode, (ErpMobile.Api.Models.Customer.AddressCreateRequest)request);
                return Created($"/api/v1/Customer/{customerCode}/addresses/{address.Id}", 
                    new ApiResponse<AddressResponse>(address, true, "Adres başarıyla oluşturuldu"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating address for customer {CustomerCode}", customerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new ApiResponse<string>("An error occurred while creating the address.", false, "Adres oluştururken bir hata oluştu", ex.Message));
            }
        }

        [HttpGet("customers/{customerCode}/contacts")]
        public async Task<ActionResult<List<ErpMobile.Api.Models.Responses.ContactResponse>>> GetContacts(string customerCode)
        {
            try
            {
                var response = await _customerStubService.GetContactsAsync(customerCode);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contacts");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("customers/{customerCode}/contacts/{contactId}")]
        public async Task<ActionResult<ErpMobile.Api.Models.Responses.ContactResponse>> GetContactById(string customerCode, string contactId)
        {
            try
            {
                var response = await _customerStubService.GetContactByIdAsync(customerCode, contactId);
                if (response == null)
                {
                    return NotFound($"Contact not found for customer {customerCode} with id: {contactId}");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contact by id");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("customers/{customerCode}/contacts")]
        public async Task<ActionResult<ErpMobile.Api.Models.Responses.ContactResponse>> CreateContact(string customerCode, [FromBody] ErpMobile.Api.Models.Contact.ContactCreateRequest request)
        {
            try
            {
                var response = await _customerStubService.CreateContactAsync(customerCode, (ErpMobile.Api.Models.Contact.ContactCreateRequest)request);
                return CreatedAtAction(nameof(GetContactById), new { customerCode = customerCode, contactId = response.ContactTypeCode }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating contact");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Kişi tiplerini getirir
        /// </summary>
        [HttpGet("contact-types")]
        public async Task<ActionResult<List<ContactTypeResponse>>> GetContactTypes()
        {
            try
            {
                var response = await _customerStubService.GetContactTypesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contact types");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Kişi tipini koda göre getirir
        /// </summary>
        [HttpGet("contact-types/{code}")]
        public async Task<ActionResult<ContactTypeResponse>> GetContactTypeByCode(string code)
        {
            try
            {
                var response = await _customerStubService.GetContactTypeByCodeAsync(code);
                if (response == null)
                {
                    return NotFound($"Contact type not found with code: {code}");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contact type by code");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Yeni müşteri oluşturma API'si - Geliştirilmiş versiyon
        /// </summary>
        /// <param name="request">Müşteri oluşturma isteği</param>
        /// <returns>Oluşturulan müşteri bilgileri</returns>
        [HttpPost("create-basic")]
        [ProducesResponseType(typeof(ApiResponse<CustomerCreateResponseNew>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomerNew()
        {
            try
            {
                _logger.LogInformation("Yeni müşteri oluşturma isteği alındı");
                
                // Form verilerinden CustomerCreateRequestNew nesnesini oluştur
                var request = new CustomerCreateRequestNew();
                
                // Form alanlarını al
                if (Request.Form.ContainsKey("CustomerCode"))
                    request.CustomerCode = Request.Form["CustomerCode"].ToString();
                
                if (Request.Form.ContainsKey("CustomerName"))
                    request.CustomerName = Request.Form["CustomerName"].ToString();
                
                if (Request.Form.ContainsKey("CustomerSurname"))
                    request.CustomerSurname = Request.Form["CustomerSurname"].ToString();
                
                if (Request.Form.ContainsKey("CustomerTypeCode") && byte.TryParse(Request.Form["CustomerTypeCode"].ToString(), out byte customerTypeCode))
                    request.CustomerTypeCode = customerTypeCode;
                else
                    request.CustomerTypeCode = 3; // Varsayılan müşteri tipi
                
                if (Request.Form.ContainsKey("CompanyCode") && short.TryParse(Request.Form["CompanyCode"].ToString(), out short companyCode))
                    request.CompanyCode = companyCode;
                else
                    request.CompanyCode = 1; // Varsayılan şirket kodu
                
                // CountryCode ve StateCode özellikleri CustomerCreateRequestNew sınıfında yok
                // Bu nedenle bu alanları atladık
                
                if (Request.Form.ContainsKey("CityCode"))
                    request.CityCode = Request.Form["CityCode"].ToString();
                
                if (Request.Form.ContainsKey("DistrictCode"))
                    request.DistrictCode = Request.Form["DistrictCode"].ToString();
                
                // Address ve ContactName özellikleri CustomerCreateRequestNew sınıfında yok
                // Bu nedenle bu alanları atladık
                
                if (Request.Form.ContainsKey("OfficeCode"))
                    request.OfficeCode = Request.Form["OfficeCode"].ToString();
                else
                    request.OfficeCode = "M"; // Varsayılan ofis kodu
                
                if (Request.Form.ContainsKey("CurrencyCode"))
                    request.CurrencyCode = Request.Form["CurrencyCode"].ToString();
                else
                    request.CurrencyCode = "TRY"; // Varsayılan para birimi
                
                if (Request.Form.ContainsKey("IsIndividualAcc") && bool.TryParse(Request.Form["IsIndividualAcc"].ToString(), out bool isIndividualAcc))
                    request.IsIndividualAcc = isIndividualAcc;
                
                if (Request.Form.ContainsKey("CreatedUserName"))
                    request.CreatedUserName = Request.Form["CreatedUserName"].ToString();
                else
                    request.CreatedUserName = "system"; // Varsayılan oluşturan kullanıcı
                
                if (Request.Form.ContainsKey("LastUpdatedUserName"))
                    request.LastUpdatedUserName = Request.Form["LastUpdatedUserName"].ToString();
                else
                    request.LastUpdatedUserName = "system"; // Varsayılan güncelleyen kullanıcı
                
                if (Request.Form.ContainsKey("TaxNumber"))
                    request.TaxNumber = Request.Form["TaxNumber"].ToString();
                
                if (Request.Form.ContainsKey("IdentityNum"))
                    request.IdentityNum = Request.Form["IdentityNum"].ToString();
                
                if (Request.Form.ContainsKey("TaxOfficeCode"))
                    request.TaxOfficeCode = Request.Form["TaxOfficeCode"].ToString();
                
                if (Request.Form.ContainsKey("IsSubjectToEInvoice") && bool.TryParse(Request.Form["IsSubjectToEInvoice"].ToString(), out bool isSubjectToEInvoice))
                    request.IsSubjectToEInvoice = isSubjectToEInvoice;
                
                if (Request.Form.ContainsKey("IsSubjectToEShipment") && bool.TryParse(Request.Form["IsSubjectToEShipment"].ToString(), out bool isSubjectToEShipment))
                    request.IsSubjectToEShipment = isSubjectToEShipment;
                
                if (Request.Form.ContainsKey("EInvoiceStartDate") && DateTime.TryParse(Request.Form["EInvoiceStartDate"].ToString(), out DateTime eInvoiceStartDate))
                    request.EInvoiceStartDate = eInvoiceStartDate;
                
                if (Request.Form.ContainsKey("EShipmentStartDate") && DateTime.TryParse(Request.Form["EShipmentStartDate"].ToString(), out DateTime eShipmentStartDate))
                    request.EShipmentStartDate = eShipmentStartDate;
                
                // Frontend'den gelen tüm verileri logla
                _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - FRONTEND'DEN GELEN VERİLER:\u001b[0m");
                _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - Müşteri Adı: {CustomerName}\u001b[0m", request.CustomerName);
                _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - Vergi Numarası: {TaxNumber}\u001b[0m", 
                    string.IsNullOrEmpty(request.TaxNumber) ? "GELMEDI" : request.TaxNumber);
                _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - Vergi Dairesi: {TaxOfficeCode}\u001b[0m", 
                    string.IsNullOrEmpty(request.TaxOfficeCode) ? "GELMEDI" : request.TaxOfficeCode);
                _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - Para Birimi: {CurrencyCode}\u001b[0m", 
                    string.IsNullOrEmpty(request.CurrencyCode) ? "GELMEDI" : request.CurrencyCode);
                _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - E-Fatura Mükellefi: {IsSubjectToEInvoice}\u001b[0m", request.IsSubjectToEInvoice);
                _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - E-Fatura Başlangıç Tarihi: {EInvoiceStartDate}\u001b[0m", 
                    request.EInvoiceStartDate.HasValue ? request.EInvoiceStartDate.Value.ToString("yyyy-MM-dd") : "GELMEDI");
                _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - E-İrsaliye Mükellefi: {IsSubjectToEShipment}\u001b[0m", request.IsSubjectToEShipment);
                _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - E-İrsaliye Başlangıç Tarihi: {EShipmentStartDate}\u001b[0m", 
                    request.EShipmentStartDate.HasValue ? request.EShipmentStartDate.Value.ToString("yyyy-MM-dd") : "GELMEDI");
                
                // Request'in JSON formatını logla
                var jsonOptions = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
                var requestJson = System.Text.Json.JsonSerializer.Serialize(request, jsonOptions);
                _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - REQUEST JSON: {RequestJson}\u001b[0m", requestJson);
                
                // E-Fatura ve E-İrsaliye başlangıç tarihlerini ayarla
                if (request.IsSubjectToEInvoice && !request.EInvoiceStartDate.HasValue)
                {
                    request.EInvoiceStartDate = DateTime.Now;
                    _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - E-Fatura başlangıç tarihi ayarlandı: {EInvoiceStartDate}\u001b[0m", request.EInvoiceStartDate);
                }
                
                if (request.IsSubjectToEShipment && !request.EShipmentStartDate.HasValue)
                {
                    request.EShipmentStartDate = DateTime.Now;
                    _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - E-İrsaliye başlangıç tarihi ayarlandı: {EShipmentStartDate}\u001b[0m", request.EShipmentStartDate);
                }
                
                // Frontend'den gelen vergi dairesi ve vergi numarası bilgilerini kontrol et
                if (string.IsNullOrEmpty(request.TaxOfficeCode))
                {
                    _logger.LogWarning("\u001b[33m[CustomerController.CreateCustomerNew] - Vergi dairesi boş geldi\u001b[0m");
                }
                
                if (string.IsNullOrEmpty(request.TaxNumber))
                {
                    _logger.LogWarning("\u001b[33m[CustomerController.CreateCustomerNew] - Vergi numarası boş geldi\u001b[0m");
                }
                
                // Para birimi kontrolü
                if (string.IsNullOrEmpty(request.CurrencyCode))
                {
                    _logger.LogWarning("\u001b[33m[CustomerController.CreateCustomerNew] - Para birimi boş geldi\u001b[0m");
                }
                else
                {
                    _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - Para birimi: {CurrencyCode}\u001b[0m", request.CurrencyCode);
                }
                
                // E-Fatura bilgilerini kontrol et
                if (request.IsSubjectToEInvoice && !request.EInvoiceStartDate.HasValue)
                {
                    _logger.LogWarning("\u001b[33m[CustomerController.CreateCustomerNew] - E-Fatura mükellefi seçili ama başlangıç tarihi boş\u001b[0m");
                }
                else if (request.IsSubjectToEInvoice)
                {
                    _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - E-Fatura bilgileri: {IsSubject}, {Date}\u001b[0m", 
                        request.IsSubjectToEInvoice, request.EInvoiceStartDate?.ToString("yyyy-MM-dd") ?? "Tarih yok");
                }
                
                // E-İrsaliye bilgilerini kontrol et
                if (request.IsSubjectToEShipment && !request.EShipmentStartDate.HasValue)
                {
                    _logger.LogWarning("\u001b[33m[CustomerController.CreateCustomerNew] - E-İrsaliye mükellefi seçili ama başlangıç tarihi boş\u001b[0m");
                }
                else if (request.IsSubjectToEShipment)
                {
                    _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - E-İrsaliye bilgileri: {IsSubject}, {Date}\u001b[0m", 
                        request.IsSubjectToEShipment, request.EShipmentStartDate?.ToString("yyyy-MM-dd") ?? "Tarih yok");
                }
                
                // Düzeltilmiş request'i logla
                var fixedRequestJson = System.Text.Json.JsonSerializer.Serialize(request, jsonOptions);
                _logger.LogInformation("\u001b[33m[CustomerController.CreateCustomerNew] - REQUEST JSON (AFTER FIX): {RequestJson}\u001b[0m", fixedRequestJson);
                    
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Geçersiz müşteri bilgileri",
                        Data = "Lütfen tüm zorunlu alanları doldurun."
                    });
                }
                
                // Müşteri kodu otomatik oluşturulacak
                if (string.IsNullOrEmpty(request.CustomerCode))
                {
                    try
                    {
                        // Müşteri tipi 3 (Customer) veya 1 (Vendor) olabilir
                        byte currAccTypeCode = 3; // Varsayılan olarak müşteri (Customer)
                        string prefix = "121."; // Müşteriler için 120.XXX formatı
                        
                        // CustomerTypeCode değerini kullan
                        currAccTypeCode = request.CustomerTypeCode;
                        // Tedarikçiler için 320.XXX formatı
                        if (currAccTypeCode == 1)
                        {
                            prefix = "320.";
                        }
                        
                        // Veritabanından son müşteri kodunu alıp bir sonraki kodu oluştur
                        using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                        {
                            await connection.OpenAsync();
                            
                            // En son kullanılan kodu bul
                            string query = $"SELECT TOP 1 CurrAccCode FROM cdCurrAcc WHERE CurrAccCode LIKE '{prefix}%' ORDER BY CurrAccCode DESC";
                            string lastCode = await connection.QueryFirstOrDefaultAsync<string>(query);
                            
                            // Yeni kod oluştur
                            int lastNumber = 0;
                            if (!string.IsNullOrEmpty(lastCode) && lastCode.Length > prefix.Length)
                            {
                                string lastNumberStr = lastCode.Substring(prefix.Length);
                                if (int.TryParse(lastNumberStr, out lastNumber))
                                {
                                    lastNumber++;
                                }
                                else
                                {
                                    lastNumber = 1;
                                }
                            }
                            else
                            {
                                lastNumber = 1;
                            }
                            
                            // Yeni kodu ayarla
                            request.CustomerCode = $"{prefix}{lastNumber:000}";
                            _logger.LogInformation("Otomatik müşteri kodu oluşturuldu: {CustomerCode}", request.CustomerCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Müşteri kodu oluşturulurken hata oluştu");
                        return StatusCode(500, new ApiResponse<string>
                        {
                            Success = false,
                            Message = "Müşteri kodu oluşturulurken hata oluştu",
                            Data = ex.Message
                        });
                    }
                }
                
                var result = await _customerServiceNew.CreateCustomerAsync(request);
                
                if (result.Success)
                {
                    return Ok(new ApiResponse<CustomerCreateResponseNew>
                    {
                        Success = true,
                        Message = "Müşteri başarıyla oluşturuldu",
                        Data = result
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse<CustomerCreateResponseNew>
                    {
                        Success = false,
                        Message = "Müşteri oluşturma işlemi başarısız",
                        Data = result
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri oluşturma sırasında hata: {ErrorMessage}", ex.Message);
                
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Müşteri oluşturma sırasında bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        // Bu metot zaten yukarıda tanımlanmış, o yüzden kaldırıldı

        /// <summary>
        /// Müşteri güncelleme
        /// </summary>
        /// <param name="request">Müşteri güncelleme isteği</param>
        /// <returns>Müşteri güncelleme yanıtı</returns>
        [HttpPost("update")]
        [ProducesResponseType(typeof(ApiResponse<CustomerUpdateResponseNew>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CustomerUpdateResponseNew>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdateRequestNew request)
        {
            try
            {
                _logger.LogInformation("Müşteri güncelleme isteği alındı: {CustomerCode}", request.CustomerCode);
                
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Geçersiz müşteri bilgileri",
                        Data = "Lütfen tüm zorunlu alanları doldurun."
                    });
                }
                
                var result = await _customerServiceNew.UpdateCustomerAsync(request);
                
                if (result.Success)
                {
                    return Ok(new ApiResponse<CustomerUpdateResponseNew>
                    {
                        Success = true,
                        Message = "Müşteri başarıyla güncellendi",
                        Data = result
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse<CustomerUpdateResponseNew>
                    {
                        Success = false,
                        Message = "Müşteri güncelleme işlemi başarısız",
                        Data = result
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri güncelleme sırasında hata: {ErrorMessage}", ex.Message);
                
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Müşteri güncelleme sırasında bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Geçici müşteri kayıt linki oluşturur.
        /// </summary>
        /// <param name="request">Geçici link oluşturma isteği.</param>
        /// <returns>Geçici link ve QR kod bilgileri.</returns>
        [HttpPost("create-temp-link")]
        [ProducesResponseType(typeof(ApiResponse<TempLinkResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTempLink([FromBody] TempLinkRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string> { Success = false, Message = "Geçersiz istek", Data = ModelState.ToString() });
            }

            try
            {
                // Kullanıcı bilgilerini al (örneğin JWT token'dan)
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "UnknownUser";
                var userName = User.Identity?.Name ?? "UnknownUser";
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "UnknownIP";
                var userAgent = HttpContext.Request.Headers["User-Agent"].ToString() ?? "UnknownAgent";

                var result = await _tempCustomerTokenService.CreateTempLinkAsync(request, userId, userName, ipAddress, userAgent);
                
                if (result.Success)
                {
                    return Ok(new ApiResponse<TempLinkResponse>(result, true, "Geçici link başarıyla oluşturuldu."));
                }
                return BadRequest(new ApiResponse<TempLinkResponse>(result, false, result.ErrorMessage ?? "Geçici link oluşturulurken bir hata oluştu."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateTempLink sırasında hata: {ErrorMessage}", ex.Message);
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Data = null,
                    Message = "An error occurred while creating the temporary link",
                    Error = ex.Message
                };
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Geçici müşteri kayıt token'ını doğrular.
        /// </summary>
        /// <param name="token">Doğrulanacak token.</param>
        /// <returns>Token doğrulama sonucu.</returns>
        [AllowAnonymous] // Bu endpoint token ile erişileceği için anonim erişime izin veriyoruz.
        [HttpGet("validate-token/{token}")]
        [ProducesResponseType(typeof(ApiResponse<TokenValidationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateTempToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(new ApiResponse<string> { Success = false, Message = "Token boş olamaz." });
            }

            try
            {
                var result = await _tempCustomerTokenService.ValidateTokenAsync(token);
                return Ok(new ApiResponse<TokenValidationResponse>(result, result.IsValid, result.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ValidateTempToken sırasında hata: {ErrorMessage}", ex.Message);
                
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(null, false, "Token doğrulanırken sunucu hatası oluştu.", ex.Message));
            }
        }

        /// <summary>
        /// Token ile müşteri oluşturma endpoint'i
        /// </summary>
        /// <param name="tokenRequest">Token ve müşteri bilgilerini içeren istek</param>
        /// <returns>Oluşturulan müşteri bilgileri</returns>
        [AllowAnonymous] // Bu endpoint token ile erişileceği için anonim erişime izin veriyoruz
        [HttpPost("create-with-token")]
        [ProducesResponseType(typeof(ApiResponse<CustomerResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomerWithToken([FromBody] CustomerWithTokenRequest tokenRequest)
        {
            try
            {
                _logger.LogInformation("Token ile müşteri oluşturma isteği alındı");
                
#if DEBUG
                // Sadece Development ortamında detaylı loglama yap
                _logger.LogInformation($"Request ContentType: {Request.ContentType}");
                
#endif
                
                // Request null kontrolü
                if (tokenRequest == null)
                {
                    _logger.LogWarning("Request null - JSON binding hatası");
                    return BadRequest(new ApiResponse<string>(null, false, "Invalid request format. Customer data is required."));
                }
                
#if DEBUG
                // Sadece Development ortamında detaylı loglama yap
                _logger.LogInformation($"Gelen istek (parsed): {System.Text.Json.JsonSerializer.Serialize(tokenRequest)}");
                _logger.LogInformation($"Token değeri: {tokenRequest.Token ?? "null"}");
                _logger.LogInformation($"CustomerData null mu: {(tokenRequest.CustomerData == null ? "Evet" : "Hayır")}");
                
                if (tokenRequest.CustomerData != null)
                {
                    _logger.LogInformation($"CustomerData içeriği: {System.Text.Json.JsonSerializer.Serialize(tokenRequest.CustomerData)}");
                }
#endif
                
                if (string.IsNullOrWhiteSpace(tokenRequest.Token))
                {
                    _logger.LogWarning("Token boş");
                    return BadRequest(new ApiResponse<string>(null, false, "Token boş olamaz."));
                }
                
                // Token geçerliliğini kontrol et
                _logger.LogInformation($"Token doğrulanıyor: {tokenRequest.Token}");
                var tokenValidationResult = await _tokenValidationService.ValidateTokenAsync(tokenRequest.Token);
                _logger.LogInformation($"Token doğrulama sonucu: {tokenValidationResult.IsValid}, Mesaj: {tokenValidationResult.Message}");
                if (!tokenValidationResult.IsValid)
                {
                    return BadRequest(new ApiResponse<string>(null, false, tokenValidationResult.Message));
                }
                
                _logger.LogInformation("Token geçerli, müşteri verileri dönüştürülüyor");
                
                // Müşteri adı zorunlu alan kontrolü
                if (string.IsNullOrWhiteSpace(tokenRequest.CustomerData.CustomerName))
                {
                    return BadRequest(new ApiResponse<string>(null, false, "Müşteri adı boş olamaz."));
                }
                
                // Türkiye için TC Kimlik/Vergi validasyonu
                string country = tokenRequest.CustomerData.CurrencyCode == "TRY" ? "TR" : "";
                bool isIndividual = tokenRequest.CustomerData.IsIndividual;
                
                if (country == "TR")
                {
                    if (isIndividual)
                    {
                        // Bireysel müşteri için TC Kimlik kontrolü
                        if (string.IsNullOrWhiteSpace(tokenRequest.CustomerData.IdentityNum))
                        {
                            return BadRequest(new ApiResponse<string>(null, false, "Türkiye için bireysel müşterilerde TC Kimlik Numarası zorunludur."));
                        }
                        
                        // TC Kimlik format kontrolü (11 haneli sayı)
                        if (!System.Text.RegularExpressions.Regex.IsMatch(tokenRequest.CustomerData.IdentityNum, @"^\d{11}$"))
                        {
                            return BadRequest(new ApiResponse<string>(null, false, "TC Kimlik Numarası 11 haneli sayı olmalıdır."));
                        }
                    }
                    else
                    {
                        // Kurumsal müşteri için vergi numarası kontrolü
                        if (string.IsNullOrWhiteSpace(tokenRequest.CustomerData.TaxNumber))
                        {
                            return BadRequest(new ApiResponse<string>(null, false, "Türkiye için kurumsal müşterilerde Vergi Numarası zorunludur."));
                        }
                        
                        // Vergi numarası format kontrolü (10 veya 11 haneli sayı)
                        if (!System.Text.RegularExpressions.Regex.IsMatch(tokenRequest.CustomerData.TaxNumber, @"^\d{10,11}$"))
                        {
                            return BadRequest(new ApiResponse<string>(null, false, "Vergi Numarası 10 veya 11 haneli sayı olmalıdır."));
                        }
                        
                        // Vergi dairesi kontrolü
                        if (string.IsNullOrWhiteSpace(tokenRequest.CustomerData.TaxOfficeCode))
                        {
                            return BadRequest(new ApiResponse<string>(null, false, "Türkiye için kurumsal müşterilerde Vergi Dairesi seçilmelidir."));
                        }
                    }
                }
                
                // Token verilerinden CustomerCreateRequestNew oluştur
                var customerCreateRequest = new CustomerCreateRequestNew
                {
                    CustomerCode = tokenRequest.CustomerData.CustomerCode ?? "",
                    CustomerName = tokenRequest.CustomerData.CustomerName ?? "",
                    CustomerSurname = tokenRequest.CustomerData.CustomerSurname ?? "",
                    CustomerTypeCode = byte.Parse(tokenRequest.CustomerData.CustomerTypeCode ?? "3"),
                    CompanyCode = short.Parse(tokenRequest.CustomerData.CompanyCode ?? "1"),
                    OfficeCode = tokenRequest.CustomerData.OfficeCode ?? "M",
                    CurrencyCode = tokenRequest.CustomerData.CurrencyCode ?? "TRY",
                    IsIndividualAcc = tokenRequest.CustomerData.IsIndividual,
                    CreatedUserName = "SYSTEM",
                    LastUpdatedUserName = "SYSTEM",
                    TaxNumber = tokenRequest.CustomerData.TaxNumber ?? "",
                    IdentityNum = tokenRequest.CustomerData.IdentityNum ?? "",
                    TaxOfficeCode = tokenRequest.CustomerData.TaxOfficeCode ?? "",
                    IsSubjectToEInvoice = false,
                    IsSubjectToEShipment = false,
                    CityCode = tokenRequest.CustomerData.CityCode ?? "",
                    DistrictCode = tokenRequest.CustomerData.DistrictCode ?? ""
                    // CustomerCreateRequestNew'da Address alanı yok
                };

                // Normal müşteri oluşturma metodunu çağır
                var result = await _customerServiceNew.CreateCustomerAsync(customerCreateRequest);
                
                // Sonuç kontrolü
                if (!result.Success)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Müşteri oluşturma başarısız",
                        Data = result.Message
                    });
                }
                
                // CustomerResponse nesnesini oluştur
                var customer = new CustomerResponse
                {
                    CustomerCode = result.CustomerCode,
                    CustomerName = customerCreateRequest.CustomerName
                };
                
                // Token'i kullanıldı olarak işaretle
                await _tokenValidationService.MarkTokenAsUsedAsync(tokenRequest.Token);
                
                _logger.LogInformation($"Token ile müşteri başarıyla oluşturuldu: {customer.CustomerCode}");
                
                // Normal create endpoint'i ile aynı yanıt formatını kullan
                var response = new ApiResponse<CustomerResponse>
                {
                    Success = true,
                    Data = customer,
                    Message = "Customer created successfully"
                };
                return CreatedAtAction(nameof(GetCustomerByCode), new { customerCode = customer.CustomerCode }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer with token");
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Data = null,
                    Message = "An error occurred while creating the customer",
                    Error = ex.Message
                };
                return StatusCode(500, response);
            }
        }
    }
} 