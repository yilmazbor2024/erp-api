using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Response model for region information.
    /// </summary>
    public class RegionResponse
    {
        /// <summary>
        /// Gets or sets the region code.
        /// </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the region.
        /// </summary>
        public string RegionDescription { get; set; }
    }
} 