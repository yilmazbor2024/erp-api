using System;

namespace erp_api.Models.Responses
{
    /// <summary>
    /// Response model for contact type information
    /// </summary>
    public class ContactTypeResponse
    {
        /// <summary>
        /// The contact type code
        /// </summary>
        public string? ContactTypeCode { get; set; }

        /// <summary>
        /// The contact type description
        /// </summary>
        public string? ContactTypeDescription { get; set; }
    }
} 