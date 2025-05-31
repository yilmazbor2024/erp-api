using System;

namespace ErpMobile.Api.Models.Product
{
    /// <summary>
    /// Ürün tipi model sınıfı
    /// </summary>
    public class ProductTypeModel
    {
        /// <summary>
        /// Ürün tipi kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Ürün tipi açıklaması
        /// </summary>
        public string Description { get; set; }
    }
}
