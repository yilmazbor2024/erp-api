using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Response model for customer type information.
    /// </summary>
    public class CustomerTypeResponse
    {
        /// <summary>
        /// Gets or sets the customer type code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the description of the customer type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the language code.
        /// </summary>
        public string LangCode { get; set; }

        /// <summary>
        /// Gets or sets whether the customer type is blocked.
        /// </summary>
        public bool IsBlocked { get; set; }
    }
} 