using System.Collections.Generic;

namespace erp_api.Models.Responses
{
    /// <summary>
    /// Response model for address list information.
    /// </summary>
    public class AddressListResponse
    {
        /// <summary>
        /// Gets or sets the customer code.
        /// </summary>
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public string CustomerName { get; set; }
        
        /// <summary>
        /// Gets or sets the list of addresses.
        /// </summary>
        public List<AddressResponse> Addresses { get; set; }
        
        /// <summary>
        /// Gets or sets the list of address types.
        /// </summary>
        public List<AddressTypeResponse> AddressTypes { get; set; }
    }
} 