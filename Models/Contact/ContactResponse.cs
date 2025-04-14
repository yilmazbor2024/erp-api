namespace erp_api.Models.Contact
{
    /// <summary>
    /// Represents a contact response based on the prCurrAccContact table
    /// </summary>
    public class ContactResponse
    {
        /// <summary>
        /// The unique identifier for the contact
        /// </summary>
        public Guid ContactID { get; set; }
        
        /// <summary>
        /// The customer code associated with this contact
        /// </summary>
        public string? CustomerCode { get; set; }
        
        /// <summary>
        /// The contact type
        /// </summary>
        public string? ContactType { get; set; }
        
        /// <summary>
        /// The contact type code
        /// </summary>
        public string? ContactTypeCode { get; set; }
        
        /// <summary>
        /// The first name of the contact
        /// </summary>
        public string? FirstName { get; set; }
        
        /// <summary>
        /// The last name of the contact
        /// </summary>
        public string? LastName { get; set; }
        
        /// <summary>
        /// The full name of the contact
        /// </summary>
        public string? FirstLastName { get; set; }
        
        /// <summary>
        /// The contact information (used for compatibility)
        /// </summary>
        public string? Contact { get; set; }
        
        /// <summary>
        /// Whether this is the default contact
        /// </summary>
        public bool IsDefault => IsAuthorized;
        
        /// <summary>
        /// Whether the contact is authorized
        /// </summary>
        public bool IsAuthorized { get; set; }
        
        /// <summary>
        /// Whether the contact is active
        /// </summary>
        public bool IsActive => !IsBlocked;
        
        /// <summary>
        /// Whether the contact is blocked
        /// </summary>
        public bool IsBlocked { get; set; }
        
        /// <summary>
        /// The identity number of the contact
        /// </summary>
        public string? IdentityNum { get; set; }
        
        /// <summary>
        /// When the contact was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// Who created the contact
        /// </summary>
        public string? CreatedBy { get; set; }
        
        /// <summary>
        /// When the contact was last updated
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        
        /// <summary>
        /// Who last updated the contact
        /// </summary>
        public string? UpdatedBy { get; set; }
    }
} 