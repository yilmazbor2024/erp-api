using System.Threading.Tasks;
using ErpApi.Models.Payments;

namespace ErpApi.Services.Interfaces
{
    /// <summary>
    /// Vadeli ödeme servisi arayüzü
    /// </summary>
    public interface ICreditPaymentService
    {
        /// <summary>
        /// Fatura için vadeli ödeme kaydı oluşturur
        /// </summary>
        /// <param name="request">Vadeli ödeme isteği</param>
        /// <returns>Vadeli ödeme yanıtı</returns>
        Task<CreditPaymentResponse> CreateCreditPaymentForInvoiceAsync(CreditPaymentRequest request);
    }
}
