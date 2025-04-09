using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trReportedSaleLine")]
    public partial class trReportedSaleLine
    {
        public trReportedSaleLine()
        {
            stItemSerialNumbers = new HashSet<stItemSerialNumber>();
            trInvoiceLineReportedSaless = new HashSet<trInvoiceLineReportedSales>();
        }

        [Key]
        [Required]
        public Guid ReportedSaleLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [Required]
        public double Qty2 { get; set; }

        [Required]
        public double InvoicedQty1 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SerialNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VatCode { get; set; }

        [Required]
        public float VatRate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PCTCode { get; set; }

        [Required]
        public float PCTRate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceCurrencyCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal PriceVI { get; set; }

        [Required]
        public Guid ReportedSaleHeaderID { get; set; }

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
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual trReportedSaleHeader trReportedSaleHeader { get; set; }
        public virtual cdBatch cdBatch { get; set; }

        public virtual ICollection<stItemSerialNumber> stItemSerialNumbers { get; set; }
        public virtual ICollection<trInvoiceLineReportedSales> trInvoiceLineReportedSaless { get; set; }
    }
}
