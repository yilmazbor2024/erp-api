using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfProductHierarchyDefault")]
    public partial class dfProductHierarchyDefault
    {
        public dfProductHierarchyDefault()
        {
        }

        [Key]
        [Required]
        public int ProductHierarchyID { get; set; }

        [Required]
        public byte ProductTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitOfMeasureCode1 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitOfMeasureCode2 { get; set; }

        [Required]
        public float UnitConvertRate { get; set; }

        [Required]
        public bool UseInternet { get; set; }

        [Required]
        public bool UsePOS { get; set; }

        [Required]
        public bool UseSerialNumber { get; set; }

        [Required]
        public bool ByWeight { get; set; }

        [Required]
        public short SupplyPeriod { get; set; }

        [Required]
        public short GuaranteePeriod { get; set; }

        [Required]
        public short ShelfLife { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemAccountGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemTaxGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemPaymentPlanGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDiscountGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemVendorGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PromotionGroupCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomsTariffNumberCode { get; set; }

        [Required]
        public bool UseColorSet { get; set; }

        [Required]
        public bool UseStore { get; set; }

        [Required]
        public bool UseRoll { get; set; }

        [Required]
        public bool UseBatch { get; set; }

        [Required]
        public bool UseManufacturing { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CareWarningTemplateCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemTextileCareTemplateCode { get; set; }

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
        public virtual dfProductHierarchy dfProductHierarchy { get; set; }
        public virtual cdItemTextileCareTemplate cdItemTextileCareTemplate { get; set; }
        public virtual cdItemVendorGr cdItemVendorGr { get; set; }
        public virtual cdCareWarningTemplate cdCareWarningTemplate { get; set; }
        public virtual cdItemAccountGr cdItemAccountGr { get; set; }
        public virtual cdPromotionGroup cdPromotionGroup { get; set; }
        public virtual bsProductType bsProductType { get; set; }
        public virtual cdItemDiscountGr cdItemDiscountGr { get; set; }
        public virtual cdItemPaymentPlanGr cdItemPaymentPlanGr { get; set; }
        public virtual cdUnitOfMeasure cdUnitOfMeasure { get; set; }
        public virtual cdCustomsTariffNumber cdCustomsTariffNumber { get; set; }
        public virtual cdItemTaxGr cdItemTaxGr { get; set; }

    }
}
