using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpExportSaleRealisition")]
    public partial class tpExportSaleRealisition
    {
        public tpExportSaleRealisition()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Required]
        public DateTime RealisitionDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string RealisitionNumber { get; set; }

        [Required]
        public DateTime CustomsReleaseDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomsReleaseNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string InvoiceCurrencyCode { get; set; }

        [Required]
        public double InvoiceExchangeRate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RealisitionCurrencyCode { get; set; }

        [Required]
        public double RealisitionExchangeRate { get; set; }

        [Required]
        public bool IsRealised { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RealisedUserName { get; set; }

        [Required]
        public DateTime RealisedDate { get; set; }

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
