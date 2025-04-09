using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfCompanyCostOfGoodsSold")]
    public partial class dfCompanyCostOfGoodsSold
    {
        public dfCompanyCostOfGoodsSold()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Required]
        public byte CostingMethodCode { get; set; }

        [Required]
        public byte CostingLevelCode { get; set; }

        [Required]
        public byte CostingVariantLevelCode { get; set; }

        [Required]
        public bool CalculateEndOfPeriodInventoryOnAverage { get; set; }

        [Required]
        public bool CalculateByBatchCode { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsItemType bsItemType { get; set; }
        public virtual bsCostingVariantLevel bsCostingVariantLevel { get; set; }
        public virtual bsCostingMethod bsCostingMethod { get; set; }
        public virtual bsCostingLevel bsCostingLevel { get; set; }

    }
}
