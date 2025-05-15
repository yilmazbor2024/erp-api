using System;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    public interface ISalesInvoiceService
    {
        /// <summary>
        /// Fatura listesini getirir
        /// </summary>
        Task<InvoiceResponseList> GetInvoicesAsync(InvoiceListRequest request);

        /// <summary>
        /// Fatura detayını getirir
        /// </summary>
        Task<InvoiceDetailResponse> GetInvoiceByIdAsync(Guid invoiceId);

        /// <summary>
        /// Yeni fatura oluşturur
        /// </summary>
        Task<InvoiceCreateResponse> CreateInvoiceAsync(InvoiceCreateRequest request);

        /// <summary>
        /// Faturayı iptal eder
        /// </summary>
        Task<bool> CancelInvoiceAsync(Guid invoiceId);

        /// <summary>
        /// Ödeme planlarını getirir
        /// </summary>
        Task<PaymentPlanListResponse> GetPaymentPlansAsync(bool? forCreditCardPlan, bool isBlocked);

        /// <summary>
        /// Öznitelik tiplerini getirir
        /// </summary>
        Task<AttributeTypeListResponse> GetAttributeTypesAsync(bool isBlocked);

        /// <summary>
        /// Öznitelik değerlerini getirir
        /// </summary>
        Task<AttributeListResponse> GetAttributesAsync(string attributeTypeCode, bool isBlocked);

        /// <summary>
        /// Ofisleri getirir
        /// </summary>
        Task<OfficeListResponse> GetOfficesAsync(bool isBlocked);

        /// <summary>
        /// Ürün boyut tiplerini getirir
        /// </summary>
        Task<ItemDimensionTypeListResponse> GetItemDimensionTypesAsync(bool isBlocked);

        /// <summary>
        /// Faturaya ait borç senetlerini getirir
        /// </summary>
        Task<DebitListResponse> GetInvoiceDebitsAsync(Guid invoiceId);
    }
} 