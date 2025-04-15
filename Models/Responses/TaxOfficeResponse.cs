using System;

namespace erp_api.Models.Responses
{
    /// <summary>
    /// Response model for tax office information.
    /// </summary>
    public class TaxOfficeResponse
    {
        /// <summary>
        /// Gets or sets the tax office code.
        /// </summary>
        public string TaxOfficeCode { get; set; }

        /// <summary>
        /// Gets or sets the city code.
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the tax office.
        /// </summary>
        public string TaxOfficeDescription { get; set; }

        /// <summary>
        /// Gets or sets whether the tax office is blocked.
        /// </summary>
        public bool IsBlocked { get; set; }
    }
} 