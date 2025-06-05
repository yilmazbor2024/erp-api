using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models
{
    [Table("TempCustomerTokens", Schema = "dbo")]
    public class TempCustomerToken
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Token { get; set; }
        
        [StringLength(50)]
        public string CustomerCode { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public DateTime ExpiresAt { get; set; }
        
        public DateTime? UsedAt { get; set; }
        
        public bool IsUsed { get; set; }
        
        [StringLength(50)]
        public string CreatedByUserId { get; set; }
        
        [StringLength(50)]
        public string CreatedByUserName { get; set; }
        
        [StringLength(50)]
        public string IpAddress { get; set; }
        
        [StringLength(255)]
        public string UserAgent { get; set; }
        
        /// <summary>
        /// Yeni müşteri kaydı için oluşturulmuş bir token ise true
        /// </summary>
        public bool IsNewCustomer { get; set; }
    }
}
