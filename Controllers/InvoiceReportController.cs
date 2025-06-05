using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Services.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    [EnableCors]
    public class InvoiceReportController : ControllerBase
    {
        private readonly IInvoiceReportService _invoiceReportService;
        private readonly ILogger<InvoiceReportController> _logger;

        public InvoiceReportController(IInvoiceReportService invoiceReportService, ILogger<InvoiceReportController> logger)
        {
            _invoiceReportService = invoiceReportService;
            _logger = logger;
        }

        /// <summary>
        /// Get invoice summary by invoice types
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summaries by type</returns>
        [HttpGet("by-type")]
        public async Task<ActionResult<ApiResponse<List<InvoiceTypeSummaryResponse>>>> GetInvoiceTypesSummary([FromQuery] InvoiceListRequest request)
        {
            try
            {
                _logger.LogInformation($"GetInvoiceTypesSummary called with ProcessCode: {request.ProcessCode}, CustomerCode: {request.CustomerCode}, VendorCode: {request.VendorCode}, StartDate: {request.StartDate}, EndDate: {request.EndDate}");

                // Remove unnecessary parameters if ProcessCode is specified
                if (!string.IsNullOrEmpty(request.ProcessCode))
                {
                    _logger.LogInformation($"Using ProcessCode filter: {request.ProcessCode}");
                    // Remove unnecessary parameters
                    request.CompanyCode = null;
                    request.StoreCode = null;
                    request.WarehouseCode = null;
                }

                var result = await _invoiceReportService.GetInvoiceTypesSummaryAsync(request);
                return Ok(new ApiResponse<List<InvoiceTypeSummaryResponse>>(result, true, "Fatura tipi özetleri başarıyla getirildi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetInvoiceTypesSummary");
                return StatusCode(500, new ApiResponse<List<InvoiceTypeSummaryResponse>>(null, false, "Fatura tipi özetleri getirilirken bir hata oluştu.", ex.Message));
            }
        }

        /// <summary>
        /// Get invoice summary by dates
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summaries by date</returns>
        [HttpGet("by-date")]
        public async Task<ActionResult<ApiResponse<List<InvoiceDateSummaryResponse>>>> GetInvoiceDatesSummary([FromQuery] InvoiceListRequest request)
        {
            try
            {
                _logger.LogInformation($"GetInvoiceDatesSummary called with ProcessCode: {request.ProcessCode}, CustomerCode: {request.CustomerCode}, VendorCode: {request.VendorCode}, StartDate: {request.StartDate}, EndDate: {request.EndDate}");

                // Remove unnecessary parameters if ProcessCode is specified
                if (!string.IsNullOrEmpty(request.ProcessCode))
                {
                    _logger.LogInformation($"Using ProcessCode filter: {request.ProcessCode}");
                    // Remove unnecessary parameters
                    request.CompanyCode = null;
                    request.StoreCode = null;
                    request.WarehouseCode = null;
                }

                var result = await _invoiceReportService.GetInvoiceDatesSummaryAsync(request);
                return Ok(new ApiResponse<List<InvoiceDateSummaryResponse>>(result, true, "Fatura tarih özetleri başarıyla getirildi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetInvoiceDatesSummary");
                return StatusCode(500, new ApiResponse<List<InvoiceDateSummaryResponse>>(null, false, "Fatura tarih özetleri getirilirken bir hata oluştu.", ex.Message));
            }
        }

        /// <summary>
        /// Get invoice summary by customers
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summaries by customer</returns>
        [HttpGet("by-customer")]
        public async Task<ActionResult<ApiResponse<List<InvoiceCustomerSummaryResponse>>>> GetInvoiceCustomersSummary([FromQuery] InvoiceListRequest request)
        {
            try
            {
                _logger.LogInformation($"GetInvoiceCustomersSummary called with ProcessCode: {request.ProcessCode}, CustomerCode: {request.CustomerCode}, VendorCode: {request.VendorCode}, StartDate: {request.StartDate}, EndDate: {request.EndDate}");

                // Remove unnecessary parameters if ProcessCode is specified
                if (!string.IsNullOrEmpty(request.ProcessCode))
                {
                    _logger.LogInformation($"Using ProcessCode filter: {request.ProcessCode}");
                    // Remove unnecessary parameters
                    request.CompanyCode = null;
                    request.StoreCode = null;
                    request.WarehouseCode = null;
                }

                var result = await _invoiceReportService.GetInvoiceCustomersSummaryAsync(request);
                return Ok(new ApiResponse<List<InvoiceCustomerSummaryResponse>>(result, true, "Fatura müşteri özetleri başarıyla getirildi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetInvoiceCustomersSummary");
                return StatusCode(500, new ApiResponse<List<InvoiceCustomerSummaryResponse>>(null, false, "Fatura müşteri özetleri getirilirken bir hata oluştu.", ex.Message));
            }
        }

        /// <summary>
        /// Get all invoice summary information
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summary information</returns>
        [HttpGet("summary")]
        public async Task<ActionResult<ApiResponse<InvoiceSummaryListResponse>>> GetInvoiceSummary([FromQuery] InvoiceListRequest request)
        {
            try
            {
                _logger.LogInformation($"GetInvoiceSummary called with ProcessCode: {request.ProcessCode}, CustomerCode: {request.CustomerCode}, VendorCode: {request.VendorCode}, StartDate: {request.StartDate}, EndDate: {request.EndDate}");

                // Remove unnecessary parameters if ProcessCode is specified
                if (!string.IsNullOrEmpty(request.ProcessCode))
                {
                    _logger.LogInformation($"Using ProcessCode filter: {request.ProcessCode}");
                    // Remove unnecessary parameters
                    request.CompanyCode = null;
                    request.StoreCode = null;
                    request.WarehouseCode = null;
                }

                var result = await _invoiceReportService.GetInvoiceSummaryAsync(request);
                return Ok(new ApiResponse<InvoiceSummaryListResponse>(result, true, "Fatura özetleri başarıyla getirildi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetInvoiceSummary");
                return StatusCode(500, new ApiResponse<InvoiceSummaryListResponse>(null, false, "Fatura özetleri getirilirken bir hata oluştu.", ex.Message));
            }
        }
    }
}
