using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Token ile müşteri oluşturma isteği modeli
    /// </summary>
    public class CustomerWithTokenRequest
    {
        /// <summary>
        /// Geçici müşteri kayıt token'ı
        /// </summary>
        [Required(ErrorMessage = "Token zorunludur")]
        public string Token { get; set; }

        /// <summary>
        /// Müşteri verileri
        /// </summary>
        [Required(ErrorMessage = "Müşteri bilgileri zorunludur")]
        public CustomerData CustomerData { get; set; }
    }

    /// <summary>
    /// Müşteri verileri modeli - CustomerCreateRequest ile aynı özelliklere sahip
    /// </summary>
    public class CustomerData
    {
        /// <summary>
        /// Gets or sets the customer code.
        /// </summary>
        [StringLength(50)]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        [Required(ErrorMessage = "Müşteri adı zorunludur")]
        [StringLength(250)]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the customer surname.
        /// </summary>
        [StringLength(100)]
        public string CustomerSurname { get; set; }

        /// <summary>
        /// Gets or sets the customer title code.
        /// </summary>
        [StringLength(10)]
        public string TitleCode { get; set; }

        /// <summary>
        /// Gets or sets the patronym (middle name).
        /// </summary>
        [StringLength(60)]
        public string Patronym { get; set; }

        /// <summary>
        /// Gets or sets the tax number.
        /// </summary>
        [StringLength(20)]
        public string TaxNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer identity number.
        /// </summary>
        [StringLength(20)]
        public string CustomerIdentityNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the identity number.
        /// </summary>
        [StringLength(20)]
        public string IdentityNum { get; set; }

        /// <summary>
        /// Gets or sets the mersis number.
        /// </summary>
        [StringLength(20)]
        public string MersisNum { get; set; }

        /// <summary>
        /// Gets or sets the customer type code.
        /// </summary>
        public string CustomerTypeCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is an individual account.
        /// </summary>
        public bool IsIndividual { get; set; }

        /// <summary>
        /// Gets or sets the discount group code.
        /// </summary>
        [StringLength(10)]
        public string DiscountGroupCode { get; set; }

        /// <summary>
        /// Gets or sets the customer markup group code.
        /// </summary>
        [StringLength(10)]
        public string CustomerMarkupGrCode { get; set; }

        /// <summary>
        /// Gets or sets the customer payment plan group code.
        /// </summary>
        [StringLength(10)]
        public string CustomerPaymentPlanGrCode { get; set; }

        /// <summary>
        /// Gets or sets the vendor payment plan group code.
        /// </summary>
        [StringLength(10)]
        public string VendorPaymentPlanGrCode { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        [StringLength(10)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the office code.
        /// </summary>
        [StringLength(10)]
        public string OfficeCode { get; set; } = "M";

        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        [StringLength(10)]
        public string CompanyCode { get; set; } = "1";

        /// <summary>
        /// Gets or sets the salesman code.
        /// </summary>
        [StringLength(20)]
        public string SalesmanCode { get; set; }

        /// <summary>
        /// Gets or sets the credit limit.
        /// </summary>
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// Gets or sets the risk limit.
        /// </summary>
        public decimal RiskLimit { get; set; }

        /// <summary>
        /// Gets or sets the city code.
        /// </summary>
        [StringLength(10)]
        public string CityCode { get; set; }

        /// <summary>
        /// Gets or sets the district code.
        /// </summary>
        [StringLength(10)]
        public string DistrictCode { get; set; }

        /// <summary>
        /// Gets or sets the tax office code.
        /// </summary>
        [StringLength(10)]
        public string TaxOfficeCode { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [StringLength(250)]
        public string Address { get; set; }

        // Not: CustomerCreateRequest sınıfında bu özellikler bulunmuyor, bu yüzden kaldırıldı
        // İletişim bilgileri muhtemelen ayrı bir API çağrısı ile ekleniyor

        /// <summary>
        /// Gets or sets the data language code.
        /// </summary>
        [StringLength(5)]
        public string DataLanguageCode { get; set; } = "TR";

        /// <summary>
        /// Gets or sets the payment term.
        /// </summary>
        public short PaymentTerm { get; set; }

        // Contact listeleri CustomerCreateRequest'te bulunmadığı için kaldırıldı
    }
}
