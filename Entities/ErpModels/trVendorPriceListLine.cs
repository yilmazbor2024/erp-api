using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trVendorPriceListLine")]
    public partial class trVendorPriceListLine
    {
        public trVendorPriceListLine()
        {
        }

        [Key]
        [Required]
        public Guid VendorPriceListLineID { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitOfMeasureCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PurchasePlanCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsDisabled { get; set; }

        [Required]
        public DateTime DisableDate { get; set; }

        [Required]
        public Guid VendorPriceListHeaderID { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdItemDim3 cdItemDim3 { get; set; }
        public virtual cdItem cdItem { get; set; }
        public virtual cdUnitOfMeasure cdUnitOfMeasure { get; set; }
        public virtual trVendorPriceListHeader trVendorPriceListHeader { get; set; }
        public virtual cdItemDim1 cdItemDim1 { get; set; }
        public virtual cdPurchasePlan cdPurchasePlan { get; set; }
        public virtual cdItemDim2 cdItemDim2 { get; set; }
        public virtual cdColor cdColor { get; set; }

    }
}
