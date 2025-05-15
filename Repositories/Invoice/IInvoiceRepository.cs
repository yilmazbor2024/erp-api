using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Invoice;

namespace ErpMobile.Api.Repositories.Invoice
{
    public interface IInvoiceRepository
    {
        // Tüm fatura tiplerini getiren genel metot
        Task<(List<InvoiceHeaderModel> items, int totalCount)> GetAllInvoicesAsync(InvoiceListRequest request);
        
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
    }
}
