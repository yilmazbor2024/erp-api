using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models.Auth
{
    [Table("AuditLogs", Schema = "dbo")]
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Action { get; set; }  // "PAGE_VIEW", "FORM_SUBMIT", "API_CALL" vb.
        
        [Required]
        public string Module { get; set; }  // "Invoice", "Customer", "Settings" vb.
        
        public string PageUrl { get; set; }
        
        public string FormName { get; set; }
        
        public string Details { get; set; } // JSON formatında detay bilgisi
        
        public string IpAddress { get; set; }
        
        public string UserAgent { get; set; }
        
        [Required]
        public DateTime Timestamp { get; set; }
    }
}
