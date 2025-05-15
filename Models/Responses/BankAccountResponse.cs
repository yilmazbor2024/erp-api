using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Banka hesabı yanıt modeli
    /// </summary>
    public class BankAccountResponse
    {
        /// <summary>
        /// Banka hesabı ID
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Banka kodu
        /// </summary>
        public string BankCode { get; set; }
        
        /// <summary>
        /// Banka adı
        /// </summary>
        public string BankName { get; set; }
        
        /// <summary>
        /// Şube kodu
        /// </summary>
        public string BranchCode { get; set; }
        
        /// <summary>
        /// Şube adı
        /// </summary>
        public string BranchName { get; set; }
        
        /// <summary>
        /// Hesap numarası
        /// </summary>
        public string AccountNumber { get; set; }
        
        /// <summary>
        /// IBAN
        /// </summary>
        public string IBAN { get; set; }
        
        /// <summary>
        /// Para birimi kodu
        /// </summary>
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Aktif mi?
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Varsayılan mı?
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
