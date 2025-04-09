using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prStoreProperties")]
    public partial class prStoreProperties
    {
        public prStoreProperties()
        {
        }

        [Key]
        [Required]
        public byte StoreTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public int UrbanPopulation { get; set; }

        [Required]
        public short StoreCarCount { get; set; }

        [Required]
        public short VitrineCount { get; set; }

        [Required]
        public short MannequinCount { get; set; }

        [Required]
        public short EmployeeCount { get; set; }

        [Required]
        public byte CashDrawerCount { get; set; }

        [Required]
        public short FloorCount { get; set; }

        [Required]
        public short EntranceCount { get; set; }

        [Required]
        public DateTime LastRemodelDate { get; set; }

        [Required]
        public byte StorePriceLevelCode { get; set; }

        [Required]
        public byte StoreClimateZoneCode { get; set; }

        [Required]
        public byte StoreConceptCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string StoreDistributionGroupCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string StoreCRMGroupCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string StoreDisplayName { get; set; }

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
        public virtual cdStoreClimateZone cdStoreClimateZone { get; set; }
        public virtual cdStorePriceLevel cdStorePriceLevel { get; set; }
        public virtual cdStoreCRMGroup cdStoreCRMGroup { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual cdStoreConcept cdStoreConcept { get; set; }
        public virtual cdStoreDistributionGroup cdStoreDistributionGroup { get; set; }

    }
}
