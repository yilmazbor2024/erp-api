using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Services.Invoice
{
    public interface IInvoiceReportService
    {
        /// <summary>
        /// Get invoice summary by invoice types
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summaries by type</returns>
        Task<List<InvoiceTypeSummaryResponse>> GetInvoiceTypesSummaryAsync(InvoiceListRequest request);

        /// <summary>
        /// Get invoice summary by dates
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summaries by date</returns>
        Task<List<InvoiceDateSummaryResponse>> GetInvoiceDatesSummaryAsync(InvoiceListRequest request);

        /// <summary>
        /// Get invoice summary by customers
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summaries by customer</returns>
        Task<List<InvoiceCustomerSummaryResponse>> GetInvoiceCustomersSummaryAsync(InvoiceListRequest request);

        /// <summary>
        /// Get all invoice summary information
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summary information</returns>
        Task<InvoiceSummaryListResponse> GetInvoiceSummaryAsync(InvoiceListRequest request);
    }
}
