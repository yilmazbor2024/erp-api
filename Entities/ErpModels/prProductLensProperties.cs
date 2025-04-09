using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prProductLensProperties")]
    public partial class prProductLensProperties
    {
        public prProductLensProperties()
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
        public string FocalTypeCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string CoatingTypeCode { get; set; }

        [Required]
        public double Sphere { get; set; }

        [Required]
        public double Cylinder { get; set; }

        [Required]
        public double GlassIndex { get; set; }

        [Required]
        public double Diameter { get; set; }

        [Required]
        public double BaseCurveRadius { get; set; }

        [Required]
        public double WaterContent { get; set; }

        [Required]
        public short DisposeFrequency { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OpticalGroupRangeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OpticalSutCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomProcessGroupCode { get; set; }

        [Required]
        public byte LensTypeCode { get; set; }

        [Required]
        public byte EyeGlassSutTypeCode { get; set; }

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
        public virtual cdItem cdItem { get; set; }
        public virtual bsLensType bsLensType { get; set; }
        public virtual cdOpticalSut cdOpticalSut { get; set; }
        public virtual cdOpticalGroupRange cdOpticalGroupRange { get; set; }
        public virtual cdBaseMaterial cdBaseMaterial { get; set; }
        public virtual cdBrand cdBrand { get; set; }
        public virtual cdCoatingType cdCoatingType { get; set; }
        public virtual cdFocalType cdFocalType { get; set; }
        public virtual cdManufacturer cdManufacturer { get; set; }
        public virtual cdCustomProcessGroup cdCustomProcessGroup { get; set; }
        public virtual bsGlassIndex bsGlassIndex { get; set; }
        public virtual bsEyeGlassSutType bsEyeGlassSutType { get; set; }

    }
}
