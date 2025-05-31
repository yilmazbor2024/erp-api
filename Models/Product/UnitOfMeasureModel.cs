using System;

namespace ErpMobile.Api.Models.Product
{
    /// <summary>
    /// Ölçü birimi model sınıfı
    /// </summary>
    public class UnitOfMeasureModel
    {
        /// <summary>
        /// Ölçü birimi kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Ölçü birimi açıklaması
        /// </summary>
        public string Description { get; set; }
    }
}
