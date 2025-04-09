using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trItemTestLine")]
    public partial class trItemTestLine
    {
        public trItemTestLine()
        {
        }

        [Key]
        [Required]
        public Guid ItemTestLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

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

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ManufacturerItemCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ManufacturerColorCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemTestTypeCode { get; set; }

        [Required]
        public double Qty { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [Required]
        public Guid InnerLineID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceCurrencyCode { get; set; }

        [Required]
        public double PriceExchangeRate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime ResultDate { get; set; }

        [Required]
        public TimeSpan ResultTime { get; set; }

        [Required]
        public bool IsPass { get; set; }

        [Required]
        public Guid ItemTestHeaderID { get; set; }

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
        public virtual trInnerLine trInnerLine { get; set; }
        public virtual cdItemTestType cdItemTestType { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trItemTestHeader trItemTestHeader { get; set; }

    }
}
