using System;

namespace ErpMobile.Api.Models.Customer
{
    /// <summary>
    /// Müşteri varsayılan adres ve iletişim bilgileri yanıt modeli
    /// </summary>
    public class CustomerDefaultAddressResponse
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CurrAccCode { get; set; }
        
        /// <summary>
        /// Müşteri tipi kodu
        /// </summary>
        public int CurrAccTypeCode { get; set; }
        
        /// <summary>
        /// Varsayılan posta adresi ID
        /// </summary>
        public Guid? PostalAddressID { get; set; }
        
        /// <summary>
        /// Varsayılan iletişim ID
        /// </summary>
        public Guid? CommunicationID { get; set; }
        
        /// <summary>
        /// Varsayılan kontak ID
        /// </summary>
        public Guid? ContactID { get; set; }
        
        /// <summary>
        /// Alt müşteri ID
        /// </summary>
        public int? SubCurrAccID { get; set; }
        
        /// <summary>
        /// E-Arşiv e-posta iletişim ID
        /// </summary>
        public Guid? EArchieveEMailCommunicationID { get; set; }
        
        /// <summary>
        /// E-Arşiv mobil iletişim ID
        /// </summary>
        public Guid? EArchieveMobileCommunicationID { get; set; }
        
        /// <summary>
        /// Ofis telefonu ID
        /// </summary>
        public Guid? OfficePhoneID { get; set; }
        
        /// <summary>
        /// Ev telefonu ID
        /// </summary>
        public Guid? HomePhoneID { get; set; }
        
        /// <summary>
        /// İş mobil telefonu ID
        /// </summary>
        public Guid? BusinessMobileID { get; set; }
        
        /// <summary>
        /// Kişisel mobil telefonu ID
        /// </summary>
        public Guid? PersonalMobileID { get; set; }
        
        /// <summary>
        /// Teslimat adresi ID
        /// </summary>
        public Guid? ShippingAddressID { get; set; }
        
        /// <summary>
        /// Fatura adresi ID
        /// </summary>
        public Guid? BillingAddressID { get; set; }
        
        /// <summary>
        /// Rehberli satış bildirim e-posta ID
        /// </summary>
        public Guid? GuidedSalesNotificationEmailID { get; set; }
        
        /// <summary>
        /// Rehberli satış bildirim telefon ID
        /// </summary>
        public Guid? GuidedSalesNotificationPhoneID { get; set; }
    }
}
