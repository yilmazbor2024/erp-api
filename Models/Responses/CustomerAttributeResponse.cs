using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Müşteri özellik bilgilerini içeren yanıt modeli
    /// </summary>
    public class CustomerAttributeResponse
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Özellik tipi kodu
        /// </summary>
        public string AttributeTypeCode { get; set; }

        /// <summary>
        /// Özellik kodu
        /// </summary>
        public string AttributeCode { get; set; }

        /// <summary>
        /// Özellik değeri
        /// </summary>
        public string AttributeValue { get; set; }

        /// <summary>
        /// Özellik açıklaması
        /// </summary>
        public string AttributeDescription { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı adı
        /// </summary>
        public string CreatedUserName { get; set; }
    }
}
