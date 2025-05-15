using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Temel müşteri oluşturma isteği
    /// </summary>
    public class CustomerCreateBasicRequest
    {
        /// <summary>
        /// Müşteri adı
        /// </summary>
        [Required]
        public string CustomerName { get; set; }

        /// <summary>
        /// Müşteri kodu (opsiyonel, boş ise otomatik oluşturulur)
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Vergi numarası
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        /// Vergi dairesi
        /// </summary>
        public string TaxOffice { get; set; }

        /// <summary>
        /// Müşteri tipi kodu
        /// </summary>
        public string CustomerTypeCode { get; set; }

        /// <summary>
        /// Müşteri grubu kodu
        /// </summary>
        public string CustomerGroupCode { get; set; }

        /// <summary>
        /// Müşteri açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Aktif mi?
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
