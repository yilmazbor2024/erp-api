using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
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
        
        /// <summary>
        /// Oluşturan kullanıcı adı - prCurrAccCommunication.CreatedUserName alanı ile eşleşir
        /// </summary>
        [StringLength(20)]
        public string CreatedUserName { get; set; }
        
        /// <summary>
        /// Son güncelleyen kullanıcı adı - prCurrAccCommunication.LastUpdatedUserName alanı ile eşleşir
        /// </summary>
        [StringLength(20)]
        public string LastUpdatedUserName { get; set; }
    }
} 