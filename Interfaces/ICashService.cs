using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models;

namespace ErpMobile.Api.Interfaces
{
    public interface ICashService
    {
        /// <summary>
        /// Tüm kasa hareketlerini getirir
        /// </summary>
        Task<List<CashTransactionResponse>> GetAllCashTransactionsAsync(string langCode = "TR");

        /// <summary>
        /// Belirli bir para birimine ait kasa hareketlerini getirir
        /// </summary>
        Task<List<CashTransactionResponse>> GetCashTransactionsByCurrencyAsync(string currencyCode, string langCode = "TR");

        /// <summary>
        /// Belirli bir hareket tipine ait kasa hareketlerini getirir
        /// </summary>
        Task<List<CashTransactionResponse>> GetCashTransactionsByTypeAsync(int cashTransTypeCode, string langCode = "TR");

        /// <summary>
        /// Belirli bir para birimi ve hareket tipine ait kasa hareketlerini getirir
        /// </summary>
        Task<List<CashTransactionResponse>> GetCashTransactionsByCurrencyAndTypeAsync(string currencyCode, int cashTransTypeCode, string langCode = "TR");

        /// <summary>
        /// Belirli bir tarih aralığındaki kasa hareketlerini getirir
        /// </summary>
        Task<List<CashTransactionResponse>> GetCashTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate, string langCode = "TR");
    }
}
