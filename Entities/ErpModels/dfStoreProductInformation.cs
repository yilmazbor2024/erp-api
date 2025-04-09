using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfStoreProductInformation")]
    public partial class dfStoreProductInformation
    {
        public dfStoreProductInformation()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCurrAccCode { get; set; }

        [Key]
        [Required]
        public byte TypeCode { get; set; }

        [Required]
        public bool ShowInventory { get; set; }

        [Required]
        public bool ShowInventoryAll { get; set; }

        [Required]
        public bool ShowOtherVariants { get; set; }

        [Required]
        public bool ShowOtherVariantsInventory { get; set; }

        [Required]
        public bool ShowOtherVariantsInventoryAll { get; set; }

        [Required]
        public bool ShowEquivalentProduct { get; set; }

        [Required]
        public bool ShowAllPrices { get; set; }

        [Required]
        public byte BasePriceCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceGroupCode { get; set; }

        [Required]
        public bool ShowNotes { get; set; }

        [Required]
        public bool ShowMeasuresOfVolume { get; set; }

        [Required]
        public bool ShowLanguages { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ShowThisAttributes { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ShowThisProductPointTypes { get; set; }

        [Required]
        public bool ShowCareWarnings { get; set; }

        [Required]
        public bool ShowOtherStoreInventory { get; set; }

        [Required]
        public bool ShowOtherStoreOtherVariantsInventory { get; set; }

        [Required]
        public bool CreateStoreOrderSFS { get; set; }

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
        public virtual dfStoreDefault dfStoreDefault { get; set; }
        public virtual cdPriceGroup cdPriceGroup { get; set; }

    }
}
