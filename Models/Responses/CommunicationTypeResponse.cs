using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Response model for communication type information.
    /// </summary>
    public class CommunicationTypeResponse
    {
        /// <summary>
        /// Gets or sets the communication type code.
        /// </summary>
        public string? CommunicationTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the communication type.
        /// </summary>
        public string? CommunicationTypeDescription { get; set; }
    }
} 