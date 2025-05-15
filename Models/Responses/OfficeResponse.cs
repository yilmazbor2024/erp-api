using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Response model for office information.
    /// </summary>
    public class OfficeResponse
    {
        /// <summary>
        /// Gets or sets the office code.
        /// </summary>
        public string OfficeCode { get; set; }

        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Gets or sets the office description.
        /// </summary>
        public string OfficeDescription { get; set; }

        /// <summary>
        /// Gets or sets whether this is an executive office.
        /// </summary>
        public bool IsExecutiveOffice { get; set; }

        /// <summary>
        /// Gets or sets whether the office is blocked.
        /// </summary>
        public bool IsBlocked { get; set; }
    }
} 