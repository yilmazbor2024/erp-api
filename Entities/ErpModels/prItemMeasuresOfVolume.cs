using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prItemMeasuresOfVolume")]
    public partial class prItemMeasuresOfVolume
    {
        public prItemMeasuresOfVolume()
        {
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SizeUnitOfMeasureCode { get; set; }

        [Required]
        public float ItemWidth { get; set; }

        [Required]
        public float ItemLength { get; set; }

        [Required]
        public float ItemHeight { get; set; }

        [Required]
        public float PackedWidth { get; set; }

        [Required]
        public float PackedLength { get; set; }

        [Required]
        public float PackedHeight { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WeightUnitOfMeasureCode { get; set; }

        [Required]
        public float PackedWeight { get; set; }

        [Required]
        public float ItemWeight { get; set; }

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
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual cdUnitOfMeasure cdUnitOfMeasure { get; set; }

    }
}
