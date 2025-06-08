using System;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Payment;
using ErpMobile.Api.Models.Common;

namespace ErpMobile.Api.Interfaces
{
    public interface IPaymentService
    {
        /// <summary>
        /// Fatura için peşin ödeme işlemi yapar
        /// </summary>
        Task<CashPaymentResponse> CreateCashPaymentForInvoiceAsync(CashPaymentRequest request, string userName);
    }
}
