using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auInvoiceTsmTransactionInfo")]
    public partial class auInvoiceTsmTransactionInfo
    {
        public auInvoiceTsmTransactionInfo()
        {
        }

        [Key]
        [Required]
        public int TsmTransactionID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public short POSTerminalID { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string InvoiceNumber { get; set; }

        [Required]
        public int GetCount { get; set; }

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
