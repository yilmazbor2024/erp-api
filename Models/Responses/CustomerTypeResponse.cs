using System;

namespace erp_api.Models.Responses
{
    /// <summary>
    /// Response model for customer type information.
    /// </summary>
    public class CustomerTypeResponse
    {
        /// <summary>
        /// Gets or sets the customer type code.
        /// </summary>
        public int CustomerTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the customer type.
        /// </summary>
        public string CustomerTypeDescription { get; set; }
    }
} 