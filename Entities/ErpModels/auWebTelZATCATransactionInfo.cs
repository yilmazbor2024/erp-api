using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auWebTelZATCATransactionInfo")]
    public partial class auWebTelZATCATransactionInfo
    {
        public auWebTelZATCATransactionInfo()
        {
        }

        [Key]
        [Required]
        public Guid WebTelZATCATransactionInfoID { get; set; }

        [Required]
        public Guid V3TransactionID { get; set; }

        [Required]
        public int InvoiceID { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object QRString { get; set; }

        public Guid? UUID { get; set; }

        public string InvoiceXML { get; set; }

        public string InvoiceHash { get; set; }

        [Required]
        public int Status { get; set; }

        public string UniqueKey { get; set; }

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
