using System;

namespace erp_api.Models.Responses
{
    /// <summary>
    /// Response model for contact information in list format.
    /// </summary>
    public class ContactListResponse
    {
        /// <summary>
        /// Gets or sets the contact ID.
        /// </summary>
        public Guid ContactID { get; set; }

        /// <summary>
        /// Gets or sets the customer account code.
        /// </summary>
        public string? CustomerCode { get; set; }

        /// <summary>
        /// Gets or sets the contact type code.
        /// </summary>
        public string? ContactTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the contact type description.
        /// </summary>
        public string? ContactTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the contact information.
        /// </summary>
        public string? Contact { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is the default contact.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this contact is authorized.
        /// </summary>
        public bool IsAuthorized { get; set; }
    }
} 