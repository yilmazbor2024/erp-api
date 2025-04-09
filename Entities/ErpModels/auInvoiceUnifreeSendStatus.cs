using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auInvoiceUnifreeSendStatus")]
    public partial class auInvoiceUnifreeSendStatus
    {
        public auInvoiceUnifreeSendStatus()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceUnifreeSendStatusID { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Target { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ServiceMethod { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string StatusCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CustomProvisionNumber { get; set; }

        [Required]
        public int RetryCount { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

    }
}
