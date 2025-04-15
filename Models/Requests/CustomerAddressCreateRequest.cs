using System.ComponentModel.DataAnnotations;

namespace erp_api.Models.Requests
{
    /// <summary>
    /// Request model for creating a new customer address
    /// </summary>
    public class CustomerAddressCreateRequest
    {
        /// <summary>
        /// The customer code
        /// </summary>
        [Required]
        [StringLength(30)]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// The address type code
        /// </summary>
        [Required]
        [StringLength(10)]
        public string AddressTypeCode { get; set; }
        
        /// <summary>
        /// The address
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Address { get; set; }
        
        /// <summary>
        /// The country code
        /// </summary>
        [Required]
        [StringLength(5)]
        public string CountryCode { get; set; }
        
        /// <summary>
        /// The state code
        /// </summary>
        [StringLength(5)]
        public string StateCode { get; set; }
        
        /// <summary>
        /// The city code
        /// </summary>
        [Required]
        [StringLength(10)]
        public string CityCode { get; set; }
        
        /// <summary>
        /// The district code
        /// </summary>
        [Required]
        [StringLength(10)]
        public string DistrictCode { get; set; }
        
        /// <summary>
        /// The postal code
        /// </summary>
        [StringLength(10)]
        public string PostalCode { get; set; }
        
        /// <summary>
        /// Indicates if this is the default address
        /// </summary>
        public bool IsDefault { get; set; }
        
        /// <summary>
        /// Indicates if this address is blocked
        /// </summary>
        public bool IsBlocked { get; set; }
    }
} 