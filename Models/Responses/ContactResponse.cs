using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Response model containing customer contact information
    /// </summary>
    public class ContactResponse
    {
        /// <summary>
        /// The unique identifier for the contact
        /// </summary>
        public int ContactID { get; set; }

        /// <summary>
        /// The customer code associated with this contact
        /// </summary>
        public string? CustomerCode { get; set; }

        /// <summary>
        /// The contact type code
        /// </summary>
        public string ContactTypeCode { get; set; }

        /// <summary>
        /// The contact type description
        /// </summary>
        public string? ContactTypeDescription { get; set; }

        /// <summary>
        /// The contact information (phone number, email, etc.)
        /// </summary>
        public string Contact { get; set; }

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

        /// <summary>
        /// The current account type code
        /// </summary>
        public int CurrAccTypeCode { get; set; }

        /// <summary>
        /// The current account code
        /// </summary>
        public string CurrAccCode { get; set; }

        /// <summary>
        /// The sub-current account ID
        /// </summary>
        public int? SubCurrAccID { get; set; }

        /// <summary>
        /// The title code
        /// </summary>
        public string TitleCode { get; set; }

        /// <summary>
        /// The job title code
        /// </summary>
        public string JobTitleCode { get; set; }

        /// <summary>
        /// The first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The first and last name
        /// </summary>
        public string FirstLastName { get; set; }

        /// <summary>
        /// The identity number
        /// </summary>
        public string IdentityNum { get; set; }

        /// <summary>
        /// Indicates if this contact is blocked
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// The created user name
        /// </summary>
        public string CreatedUserName { get; set; }

        /// <summary>
        /// The last updated user name
        /// </summary>
        public string LastUpdatedUserName { get; set; }

        /// <summary>
        /// The last updated date
        /// </summary>
        public DateTime? LastUpdatedDate { get; set; }
    }
} 