using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Payment;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    public interface IPaymentService
    {
        /// <summary>
        /// Fatura için peşin ödeme işlemi yapar
        /// </summary>
        Task<CashPaymentResponse> CreateCashPaymentForInvoiceAsync(CashPaymentRequest request, string userName);
        
        /// <summary>
        /// Kasa hareketleri raporunu getirir
        /// </summary>
        Task<PagedApiResponse<CashTransactionReportItem>> GetCashTransactionReportAsync(CashTransactionReportRequest request);

        /// <summary>
        /// Kasa tahsilat fişlerini getirir
        /// </summary>
        Task<PagedApiResponse<ErpMobile.Api.Models.CashTransactionResponse>> GetCashReceiptVouchersAsync(string startDate, string endDate, string cashAccountCode, int page, int pageSize, string userName);
        
        /// <summary>
        /// Kasa tediye fişlerini getirir
        /// </summary>
        Task<PagedApiResponse<ErpMobile.Api.Models.CashTransactionResponse>> GetCashPaymentVouchersAsync(string startDate, string endDate, string cashAccountCode, int page, int pageSize, string userName);
        
        /// <summary>
        /// Kasa virman fişlerini getirir
        /// </summary>
        Task<PagedApiResponse<ErpMobile.Api.Models.CashTransactionResponse>> GetCashTransferVouchersAsync(string startDate, string endDate, string cashAccountCode, int page, int pageSize, string userName);
        
        /// <summary>
        /// Kasa bakiyelerini getirir
        /// </summary>
        Task<PagedApiResponse<CashBalanceResponse>> GetCashBalancesAsync(string startDate, string endDate, string cashAccountCode, string userName, int page, int pageSize, string searchText = null, string cashTransTypeCode = null);
        
        /// <summary>
        /// Kasa özet bilgilerini getirir (bugünün tarihi için)
        /// </summary>
        Task<ApiResponse<IEnumerable<CashSummaryResponse>>> GetCashSummaryAsync(string cashAccountCode = null, string userName = null);
    }
}
