using System;

namespace erp_api.Models.Responses
{
    /// <summary>
    /// Response model for address type information.
    /// </summary>
    public class AddressTypeResponse
    {
        /// <summary>
        /// Gets or sets the address type code.
        /// </summary>
        public string? AddressTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the address type.
        /// </summary>
        public string? AddressTypeDescription { get; set; }
    }
} 