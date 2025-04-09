using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpSupportResolveMaterial")]
    public partial class tpSupportResolveMaterial
    {
        public tpSupportResolveMaterial()
        {
        }

        [Key]
        [Required]
        public Guid SupportResolveMaterialID { get; set; }

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
        public string UsedBarcode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SerialNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [Required]
        public double Qty2 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceCurrencyCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsTaxIncluded { get; set; }

        [Required]
        public Guid SupportResolveID { get; set; }

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
        public virtual tpSupportResolve tpSupportResolve { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdBatch cdBatch { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }

    }
}
