using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prProductFrameProperties")]
    public partial class prProductFrameProperties
    {
        public prProductFrameProperties()
        {
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public byte ProductTypeCode { get; set; }

  
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BrandCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ManufacturerCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string BaseMaterialCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string FrameTypeCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string FrameShapeTypeCode { get; set; }

        [Required]
        public double BaseCurveRadius { get; set; }

        [Required]
        public double LensWidth { get; set; }

        [Required]
        public double LensHeight { get; set; }

        [Required]
        public double BridgeWidth { get; set; }

        [Required]
        public double TempleLength { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OpticalSutCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomProcessGroupCode { get; set; }

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
        public virtual cdOpticalSut cdOpticalSut { get; set; }
        public virtual cdItem cdItem { get; set; }
        public virtual cdFrameType cdFrameType { get; set; }
        public virtual cdFrameShapeType cdFrameShapeType { get; set; }
        public virtual cdBrand cdBrand { get; set; }
        public virtual cdBaseMaterial cdBaseMaterial { get; set; }
        public virtual cdCustomProcessGroup cdCustomProcessGroup { get; set; }
        public virtual cdManufacturer cdManufacturer { get; set; }

    }
}
