using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Extended response model for address type information with additional properties.
    /// </summary>
    public class ExtendedAddressTypeResponse : AddressTypeResponse
    {
        /// <summary>
        /// Gets or sets whether the address type is required.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets whether the address type is a shipping address.
        /// </summary>
        public bool IsShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets whether the address type is a billing address.
        /// </summary>
        public bool IsBillingAddress { get; set; }
    }
} 