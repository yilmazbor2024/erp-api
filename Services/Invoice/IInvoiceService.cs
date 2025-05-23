using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Invoice;

namespace ErpMobile.Api.Services.Invoice
{
    public interface IInvoiceService
    {
        // Fatura numarası oluşturan metot
        Task<ApiResponse<string>> GenerateInvoiceNumberAsync(string processCode);
        
        // Tüm fatura tiplerini getiren genel metot
        Task<ApiResponse<InvoiceListResult>> GetAllInvoicesAsync(InvoiceListRequest request);
        
        // Sipariş bazlı faturalar
        Task<(List<InvoiceHeaderModel> items, int totalCount)> GetOrderBasedInvoicesAsync(InvoiceListRequest request);
        
        // Sipariş bazlı alış faturaları
        Task<(List<InvoiceHeaderModel> items, int totalCount)> GetOrderBasedPurchaseInvoicesAsync(InvoiceListRequest request);
        
        // İrsaliye bazlı faturalar
        Task<(List<InvoiceHeaderModel> items, int totalCount)> GetShipmentBasedInvoicesAsync(InvoiceListRequest request);
        
        // İrsaliye bazlı alış faturaları
        Task<(List<InvoiceHeaderModel> items, int totalCount)> GetShipmentBasedPurchaseInvoicesAsync(InvoiceListRequest request);
        
        // Direkt toptan alış faturaları
        Task<(List<InvoiceHeaderModel> items, int totalCount)> GetDirectWholesalePurchaseInvoicesAsync(InvoiceListRequest request);
        
        // Direkt satış faturaları
        Task<(List<InvoiceHeaderModel> items, int totalCount)> GetDirectSalesInvoicesAsync(InvoiceListRequest request);
        
        // Toptan satış faturaları
        Task<ApiResponse<InvoiceListResult>> GetWholesaleInvoicesAsync(InvoiceListRequest request);
        Task<ApiResponse<InvoiceHeaderModel>> GetWholesaleInvoiceByIdAsync(int invoiceHeaderId);
        Task<ApiResponse<InvoiceHeaderModel>> CreateWholesaleInvoiceAsync(CreateInvoiceRequest request);
        
        // Toptan alış faturaları
        Task<ApiResponse<InvoiceListResult>> GetWholesalePurchaseInvoicesAsync(InvoiceListRequest request);
        Task<ApiResponse<InvoiceHeaderModel>> GetWholesalePurchaseInvoiceByIdAsync(int invoiceHeaderId);
        Task<ApiResponse<InvoiceHeaderModel>> CreateWholesalePurchaseInvoiceAsync(CreateInvoiceRequest request);
        
        // Masraf faturaları
        Task<ApiResponse<InvoiceListResult>> GetExpenseInvoicesAsync(InvoiceListRequest request);
        Task<ApiResponse<InvoiceHeaderModel>> GetExpenseInvoiceByIdAsync(int invoiceHeaderId);
        Task<ApiResponse<InvoiceHeaderModel>> CreateExpenseInvoiceAsync(CreateInvoiceRequest request);
        
        // Fatura detayları
        Task<ApiResponse<List<InvoiceDetailModel>>> GetInvoiceDetailsAsync(int invoiceHeaderId);
        
        // Fatura ödeme detayları
        Task<ApiResponse<List<InvoicePaymentDetailModel>>> GetInvoicePaymentDetailsAsync(string invoiceHeaderId);
    }
}
