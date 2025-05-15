using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Models.Common;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SalesInvoiceController : ControllerBase
    {
        private readonly ISalesInvoiceService _invoiceService;
        private readonly ILogger<SalesInvoiceController> _logger;

        public SalesInvoiceController(
            ISalesInvoiceService invoiceService,
            ILogger<SalesInvoiceController> logger)
        {
            _invoiceService = invoiceService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<InvoiceResponseList>>> GetInvoices(
            [FromQuery] int pageSize = 10, 
            [FromQuery] int pageNumber = 1,
            [FromQuery] string sortBy = "invoiceDate",
            [FromQuery] string sortDirection = "desc",
            [FromQuery] string customerCode = null,
            [FromQuery] string invoiceNumber = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] string status = null)
        {
            try
            {
                var request = new InvoiceListRequest
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    SortBy = sortBy,
                    SortDirection = sortDirection,
                    CustomerCode = customerCode,
                    InvoiceNumber = invoiceNumber,
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = status
                };

                var response = await _invoiceService.GetInvoicesAsync(request);
                return Ok(new ApiResponse<InvoiceResponseList>(response, true, "Invoices retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting invoices");
                return StatusCode(500, new ApiResponse<InvoiceResponseList>(null, false, "An error occurred while retrieving invoice list.", "InternalServerError"));
            }
        }

        [HttpGet("{invoiceId}")]
        public async Task<ActionResult<ApiResponse<InvoiceDetailResponse>>> GetInvoiceById(Guid invoiceId)
        {
            try
            {
                var response = await _invoiceService.GetInvoiceByIdAsync(invoiceId);
                if (response == null)
                {
                    return NotFound(new ApiResponse<InvoiceDetailResponse>(null, false, $"Invoice with id {invoiceId} not found.", "NotFound"));
                }

                return Ok(new ApiResponse<InvoiceDetailResponse>(response, true, "Invoice details retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting invoice with id {InvoiceId}", invoiceId);
                return StatusCode(500, new ApiResponse<InvoiceDetailResponse>(null, false, $"An error occurred while retrieving invoice with id {invoiceId}.", "InternalServerError"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<InvoiceCreateResponse>>> CreateInvoice([FromBody] InvoiceCreateRequest request)
        {
            try
            {
                var response = await _invoiceService.CreateInvoiceAsync(request);
                return Created($"api/v1/salesinvoice/{response.InvoiceId}", new ApiResponse<InvoiceCreateResponse>(response, true, "Invoice created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating invoice");
                return StatusCode(500, new ApiResponse<InvoiceCreateResponse>(null, false, "An error occurred while creating invoice.", "InternalServerError"));
            }
        }

        [HttpPatch("{invoiceId}/cancel")]
        public async Task<ActionResult<ApiResponse<bool>>> CancelInvoice(Guid invoiceId)
        {
            try
            {
                var success = await _invoiceService.CancelInvoiceAsync(invoiceId);
                if (!success)
                {
                    return NotFound(new ApiResponse<bool>(false, false, $"Invoice with id {invoiceId} not found or cannot be canceled.", "NotFound"));
                }

                return Ok(new ApiResponse<bool>(true, true, "Invoice canceled successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error canceling invoice with id {InvoiceId}", invoiceId);
                return StatusCode(500, new ApiResponse<bool>(false, false, $"An error occurred while canceling invoice with id {invoiceId}.", "InternalServerError"));
            }
        }

        [HttpGet("payment-plans")]
        public async Task<ActionResult<ApiResponse<PaymentPlanListResponse>>> GetPaymentPlans(
            [FromQuery] bool? forCreditCardPlan = null,
            [FromQuery] bool isBlocked = false)
        {
            try
            {
                var response = await _invoiceService.GetPaymentPlansAsync(forCreditCardPlan, isBlocked);
                return Ok(new ApiResponse<PaymentPlanListResponse>(response, true, "Payment plans retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting payment plans");
                return StatusCode(500, new ApiResponse<PaymentPlanListResponse>(null, false, "An error occurred while retrieving payment plans.", "InternalServerError"));
            }
        }

        [HttpGet("attribute-types")]
        public async Task<ActionResult<ApiResponse<AttributeTypeListResponse>>> GetAttributeTypes(
            [FromQuery] bool isBlocked = false)
        {
            try
            {
                var response = await _invoiceService.GetAttributeTypesAsync(isBlocked);
                return Ok(new ApiResponse<AttributeTypeListResponse>(response, true, "Attribute types retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting attribute types");
                return StatusCode(500, new ApiResponse<AttributeTypeListResponse>(null, false, "An error occurred while retrieving attribute types.", "InternalServerError"));
            }
        }

        [HttpGet("attribute-types/{attributeTypeCode}/attributes")]
        public async Task<ActionResult<ApiResponse<AttributeListResponse>>> GetAttributes(
            string attributeTypeCode,
            [FromQuery] bool isBlocked = false)
        {
            try
            {
                var response = await _invoiceService.GetAttributesAsync(attributeTypeCode, isBlocked);
                return Ok(new ApiResponse<AttributeListResponse>(response, true, "Attributes retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting attributes for type {AttributeTypeCode}", attributeTypeCode);
                return StatusCode(500, new ApiResponse<AttributeListResponse>(null, false, $"An error occurred while retrieving attributes for type {attributeTypeCode}.", "InternalServerError"));
            }
        }

        [HttpGet("offices")]
        public async Task<ActionResult<ApiResponse<OfficeListResponse>>> GetOffices(
            [FromQuery] bool isBlocked = false)
        {
            try
            {
                var response = await _invoiceService.GetOfficesAsync(isBlocked);
                return Ok(new ApiResponse<OfficeListResponse>(response, true, "Offices retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting offices");
                return StatusCode(500, new ApiResponse<OfficeListResponse>(null, false, "An error occurred while retrieving offices.", "InternalServerError"));
            }
        }

        [HttpGet("item-dimension-types")]
        public async Task<ActionResult<ApiResponse<ItemDimensionTypeListResponse>>> GetItemDimensionTypes(
            [FromQuery] bool isBlocked = false)
        {
            try
            {
                var response = await _invoiceService.GetItemDimensionTypesAsync(isBlocked);
                return Ok(new ApiResponse<ItemDimensionTypeListResponse>(response, true, "Item dimension types retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting item dimension types");
                return StatusCode(500, new ApiResponse<ItemDimensionTypeListResponse>(null, false, "An error occurred while retrieving item dimension types.", "InternalServerError"));
            }
        }

        [HttpGet("{invoiceId}/debits")]
        public async Task<ActionResult<ApiResponse<DebitListResponse>>> GetInvoiceDebits(Guid invoiceId)
        {
            try
            {
                var response = await _invoiceService.GetInvoiceDebitsAsync(invoiceId);
                return Ok(new ApiResponse<DebitListResponse>(response, true, "Invoice debits retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting debits for invoice {InvoiceId}", invoiceId);
                return StatusCode(500, new ApiResponse<DebitListResponse>(null, false, $"An error occurred while retrieving debits for invoice {invoiceId}.", "InternalServerError"));
            }
        }
    }
} 