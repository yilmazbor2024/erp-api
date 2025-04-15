using System.ComponentModel.DataAnnotations;

namespace erp_api.Models.Requests
{
    /// <summary>
    /// Request model for creating a new customer communication
    /// </summary>
    public class CustomerCommunicationCreateRequest
    {
        /// <summary>
        /// The customer code
        /// </summary>
        [Required]
        [StringLength(30)]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// The communication type code
        /// </summary>
        [Required]
        [StringLength(10)]
        public string CommunicationTypeCode { get; set; }
        
        /// <summary>
        /// The communication value (phone number, email, etc.)
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Communication { get; set; }
        
        /// <summary>
        /// Indicates if this is the default communication method
        /// </summary>
        public bool IsDefault { get; set; }
    }
} 