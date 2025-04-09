using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trTFRSInvoiceAdjustment")]
    public partial class trTFRSInvoiceAdjustment
    {
        public trTFRSInvoiceAdjustment()
        {
        }

        [Key]
        [Required]
        public Guid TFRSInvoiceAdjustmentID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Required]
        public Guid InvoiceLineID { get; set; }

        [Required]
        public short ValidYear { get; set; }

        [Required]
        public byte ValidMonth { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public float LateChargeRate { get; set; }

        [Required]
        public decimal LateChargeAmount { get; set; }

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

        // Navigation Properties
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }

    }
}
