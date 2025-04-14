using System;

namespace erp_api.Models.Responses
{
    /// <summary>
    /// Response model for customer discount group information.
    /// </summary>
    public class CustomerDiscountGroupResponse
    {
        /// <summary>
        /// Gets or sets the discount group code.
        /// </summary>
        public string DiscountGroupCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the discount group.
        /// </summary>
        public string DiscountGroupDescription { get; set; }
    }
} 