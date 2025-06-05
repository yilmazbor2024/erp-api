using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Repositories.Invoice;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services.Invoice
{
    public class InvoiceReportService : IInvoiceReportService
    {
        private readonly IInvoiceReportRepository _invoiceReportRepository;
        private readonly ILogger<InvoiceReportService> _logger;

        public InvoiceReportService(IInvoiceReportRepository invoiceReportRepository, ILogger<InvoiceReportService> logger)
        {
            _invoiceReportRepository = invoiceReportRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get invoice summary by invoice types
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summaries by type</returns>
        public async Task<List<InvoiceTypeSummaryResponse>> GetInvoiceTypesSummaryAsync(InvoiceListRequest request)
        {
            try
            {
                _logger.LogInformation($"Getting invoice type summaries with parameters: ProcessCode={request.ProcessCode}, CustomerCode={request.CustomerCode}, StartDate={request.StartDate}, EndDate={request.EndDate}");
                return await _invoiceReportRepository.GetInvoiceTypesSummaryAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetInvoiceTypesSummaryAsync");
                throw;
            }
        }

        /// <summary>
        /// Get invoice summary by dates
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summaries by date</returns>
        public async Task<List<InvoiceDateSummaryResponse>> GetInvoiceDatesSummaryAsync(InvoiceListRequest request)
        {
            try
            {
                _logger.LogInformation($"Getting invoice date summaries with parameters: ProcessCode={request.ProcessCode}, CustomerCode={request.CustomerCode}, StartDate={request.StartDate}, EndDate={request.EndDate}");
                return await _invoiceReportRepository.GetInvoiceDatesSummaryAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetInvoiceDatesSummaryAsync");
                throw;
            }
        }

        /// <summary>
        /// Get invoice summary by customers
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summaries by customer</returns>
        public async Task<List<InvoiceCustomerSummaryResponse>> GetInvoiceCustomersSummaryAsync(InvoiceListRequest request)
        {
            try
            {
                _logger.LogInformation($"Getting invoice customer summaries with parameters: ProcessCode={request.ProcessCode}, CustomerCode={request.CustomerCode}, StartDate={request.StartDate}, EndDate={request.EndDate}");
                return await _invoiceReportRepository.GetInvoiceCustomersSummaryAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetInvoiceCustomersSummaryAsync");
                throw;
            }
        }

        /// <summary>
        /// Get all invoice summary information
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summary information</returns>
        public async Task<InvoiceSummaryListResponse> GetInvoiceSummaryAsync(InvoiceListRequest request)
        {
            try
            {
                _logger.LogInformation($"Getting complete invoice summary with parameters: ProcessCode={request.ProcessCode}, CustomerCode={request.CustomerCode}, StartDate={request.StartDate}, EndDate={request.EndDate}");
                return await _invoiceReportRepository.GetInvoiceSummaryAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetInvoiceSummaryAsync");
                throw;
            }
        }
    }
}
