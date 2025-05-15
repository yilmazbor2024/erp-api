using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Represents a request to create a new contact for a customer
    /// </summary>
    public class ContactCreateRequest
    {
        /// <summary>
        /// The customer type code
        /// </summary>
        public byte CurrAccTypeCode { get; set; } = 1; // Default to 1 for Customer

        /// <summary>
        /// The contact type code
        /// </summary>
        [Required]
        public string? ContactTypeCode { get; set; }

        /// <summary>
        /// The contact information (value)
        /// </summary>
        [Required]
        public string? Contact { get; set; }

        /// <summary>
        /// Indicates whether this is the default contact for the customer
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Indicates whether this contact is authorized
        /// </summary>
        public bool IsAuthorized { get; set; }

        /// <summary>
        /// The sub-customer ID if applicable
        /// </summary>
        public Guid? SubCurrAccID { get; set; }

        /// <summary>
        /// Optional notes for this contact
        /// </summary>
        public string? Notes { get; set; }
    }
} 