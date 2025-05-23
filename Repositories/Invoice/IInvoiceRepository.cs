using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Invoice;

namespace ErpMobile.Api.Repositories.Invoice
{
    public interface IInvoiceRepository
    {
        // Fatura numarası oluşturmak için son fatura numarasını getiren metot
        Task<string> GetLastInvoiceNumberByProcessCodeAsync(string processCode);
        
        // Tüm fatura tiplerini getiren genel metot
        Task<(List<InvoiceHeaderModel> items, int totalCount)> GetAllInvoicesAsync(InvoiceListRequest request);
        
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
        Task<(List<InvoiceHeaderModel> items, int totalCount)> GetWholesaleInvoicesAsync(InvoiceListRequest request);
        Task<InvoiceHeaderModel> GetWholesaleInvoiceByIdAsync(int invoiceHeaderId);
        Task<InvoiceHeaderModel> CreateWholesaleInvoiceAsync(CreateInvoiceRequest request);
        
        // Toptan alış faturaları
        Task<(List<InvoiceHeaderModel> items, int totalCount)> GetWholesalePurchaseInvoicesAsync(InvoiceListRequest request);
        Task<InvoiceHeaderModel> GetWholesalePurchaseInvoiceByIdAsync(int invoiceHeaderId);
        Task<InvoiceHeaderModel> CreateWholesalePurchaseInvoiceAsync(CreateInvoiceRequest request);
        
        // Masraf faturaları
        Task<(List<InvoiceHeaderModel> items, int totalCount)> GetExpenseInvoicesAsync(InvoiceListRequest request);
        Task<InvoiceHeaderModel> GetExpenseInvoiceByIdAsync(int invoiceHeaderId);
        Task<InvoiceHeaderModel> CreateExpenseInvoiceAsync(CreateInvoiceRequest request);
        
        // Fatura detayları
        Task<List<InvoiceDetailModel>> GetInvoiceDetailsAsync(int invoiceHeaderId);
        
        // Fatura ödeme detayları
        Task<List<InvoicePaymentDetailModel>> GetInvoicePaymentDetailsAsync(string invoiceHeaderId);
    }
}
