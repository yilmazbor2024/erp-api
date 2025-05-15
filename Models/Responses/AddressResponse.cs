using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Response model for address information.
    /// </summary>
    public class AddressResponse
    {
        /// <summary>
        /// Gets or sets the address ID.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the customer code.
        /// </summary>
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Gets or sets the address type code.
        /// </summary>
        public string AddressTypeCode { get; set; }
        
        /// <summary>
        /// Gets or sets the address type name.
        /// </summary>
        public string AddressTypeName { get; set; }
        
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        public string District { get; set; }
        
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this is the default address.
        /// </summary>
        public bool IsDefault { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the address is active.
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// Gets or sets the creator's username.
        /// </summary>
        public string CreatedUserName { get; set; }
        
        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        public DateTime? LastUpdatedDate { get; set; }
        
        /// <summary>
        /// Gets or sets the last updater's username.
        /// </summary>
        public string LastUpdatedUserName { get; set; }
    }
} 