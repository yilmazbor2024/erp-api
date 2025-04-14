using System.ComponentModel.DataAnnotations;

namespace erp_api.Models.Requests
{
    /// <summary>
    /// Request model for updating an existing contact for a customer
    /// </summary>
    public class ContactUpdateRequest
    {
        /// <summary>
        /// The type code of the contact
        /// </summary>
        [Required]
        public string? ContactTypeCode { get; set; }

        /// <summary>
        /// The contact information (e.g., phone number, email, etc.)
        /// </summary>
        [Required]
        public string? Contact { get; set; }

        /// <summary>
        /// Indicates if this is the default contact for the customer
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Indicates if this contact is authorized
        /// </summary>
        public bool IsAuthorized { get; set; }

        /// <summary>
        /// Gets or sets notes for this contact
        /// </summary>
        public string? Notes { get; set; }
    }
} 