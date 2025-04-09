using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prOnlineDBSLimit")]
    public partial class prOnlineDBSLimit
    {
        public prOnlineDBSLimit()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OnlineDBSWebServiceCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerDBSAccountCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string BankName { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object DealerName { get; set; }

        [Required]
        public decimal TotalBalance { get; set; }

        [Required]
        public decimal UsedBalance { get; set; }

        [Required]
        public decimal BlankBalance { get; set; }

        [Required]
        public decimal UnpaidInvoiceAmount { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Currency { get; set; }

        [Required]
        public DateTime LastUpdateDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Status { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdBank cdBank { get; set; }
        public virtual cdOnlineDBSWebService cdOnlineDBSWebService { get; set; }

    }
}
