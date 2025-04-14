using System;

namespace erp_api.Models.Responses
{
    /// <summary>
    /// Response model containing customer contact information
    /// </summary>
    public class ContactResponse
    {
        /// <summary>
        /// The unique identifier for the contact
        /// </summary>
        public string? ContactID { get; set; }

        /// <summary>
        /// The customer code associated with this contact
        /// </summary>
        public string? CustomerCode { get; set; }

        /// <summary>
        /// The contact type code
        /// </summary>
        public string? ContactTypeCode { get; set; }

        /// <summary>
        /// The contact type description
        /// </summary>
        public string? ContactTypeDescription { get; set; }

        /// <summary>
        /// The contact information (phone number, email, etc.)
        /// </summary>
        public string? Contact { get; set; }

        /// <summary>
        /// Indicates if this is the default contact
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Indicates if this contact is authorized
        /// </summary>
        public bool IsAuthorized { get; set; }

        /// <summary>
        /// The date when the contact was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
} 