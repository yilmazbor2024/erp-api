using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Customer;
using erp_api.Models.Contact;
using ErpMobile.Api.Services;
using erp_api.Models.Responses;
using erp_api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.Data.SqlClient;
using ErpMobile.Api.Data;
using ErpMobile.Api.Interfaces;
using erp_api.Models.Common;
using Microsoft.AspNetCore.Http;

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

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger, ErpDbContext context)
        {
            _customerService = customerService;
            _logger = logger;
            _context = context;
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
        /// Yeni müşteri oluşturur
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> CreateCustomer([FromBody] CustomerCreateRequest request)
        {
            try
            {
                var customer = await _customerService.CreateCustomerAsync(request);
                return CreatedAtAction(nameof(GetCustomerByCode), new { customerCode = customer.CustomerCode }, customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating customer. Request: {@Request}", request);
                return StatusCode(500, "An error occurred while processing your request.");
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
                var addresses = await _customerService.GetCustomerAddressesAsync(customerCode);
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
                var communications = await _customerService.GetCustomerCommunicationsAsync(customerCode);
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
                var contacts = await _customerService.GetCustomerContactsAsync(customerCode);
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

        [HttpGet("address-types")]
        public async Task<ActionResult<List<AddressTypeResponse>>> GetAddressTypes()
        {
            try
            {
                var response = await _customerService.GetAddressTypesAsync();
                return Ok(response);
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
                var response = await _customerService.GetAddressTypeByCodeAsync(code);
                if (response == null)
                {
                    return NotFound($"Address type not found with code: {code}");
                }
                return Ok(response);
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
                var response = await _customerService.CreateAddressTypeAsync(request);
                return CreatedAtAction(nameof(GetAddressTypeByCode), new { code = response.AddressTypeCode }, response);
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
                var response = await _customerService.GetAddressesAsync(customerCode);
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
                var response = await _customerService.GetAddressByIdAsync(customerCode, addressId);
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

        [HttpPost("customers/{customerCode}/addresses")]
        public async Task<ActionResult<AddressResponse>> CreateAddress(string customerCode, [FromBody] AddressCreateRequest request)
        {
            try
            {
                if (customerCode != request.CustomerCode)
                {
                    return BadRequest("Customer code mismatch");
                }

                var response = await _customerService.CreateAddressAsync(request);
                return CreatedAtAction(nameof(GetAddressById), new { customerCode = customerCode, addressId = response.AddressTypeCode }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating address");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("customers/{customerCode}/contacts")]
        public async Task<ActionResult<List<erp_api.Models.Responses.ContactResponse>>> GetContacts(string customerCode)
        {
            try
            {
                var response = await _customerService.GetContactsAsync(customerCode);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contacts");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("customers/{customerCode}/contacts/{contactId}")]
        public async Task<ActionResult<erp_api.Models.Responses.ContactResponse>> GetContactById(string customerCode, string contactId)
        {
            try
            {
                var response = await _customerService.GetContactByIdAsync(customerCode, contactId);
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
        public async Task<ActionResult<erp_api.Models.Responses.ContactResponse>> CreateContact(string customerCode, [FromBody] ContactCreateRequest request)
        {
            try
            {
                if (customerCode != request.CustomerCode)
                {
                    return BadRequest("Customer code mismatch");
                }

                var response = await _customerService.CreateContactAsync(request);
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
                var response = await _customerService.GetContactTypesAsync();
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
                var response = await _customerService.GetContactTypeByCodeAsync(code);
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
    }
} 