using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdOpticalSut")]
    public partial class cdOpticalSut
    {
        public cdOpticalSut()
        {
            cdOpticalSutDescs = new HashSet<cdOpticalSutDesc>();
            prOpticalSutContributionAmounts = new HashSet<prOpticalSutContributionAmount>();
            prProductFramePropertiess = new HashSet<prProductFrameProperties>();
            prProductLensPropertiess = new HashSet<prProductLensProperties>();
            tpInvoiceLineOpticalProductInfos = new HashSet<tpInvoiceLineOpticalProductInfo>();
            trOrderOpticalProductLines = new HashSet<trOrderOpticalProductLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OpticalSutCode { get; set; }

        [Required]
        public byte EyeGlassSutTypeCode { get; set; }

        [Required]
        public double MinSphere { get; set; }

        [Required]
        public double MaxSphere { get; set; }

        [Required]
        public double MinCylinder { get; set; }

        [Required]
        public double MaxCylinder { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual bsEyeGlassSutType bsEyeGlassSutType { get; set; }

        public virtual ICollection<cdOpticalSutDesc> cdOpticalSutDescs { get; set; }
        public virtual ICollection<prOpticalSutContributionAmount> prOpticalSutContributionAmounts { get; set; }
        public virtual ICollection<prProductFrameProperties> prProductFramePropertiess { get; set; }
        public virtual ICollection<prProductLensProperties> prProductLensPropertiess { get; set; }
        public virtual ICollection<tpInvoiceLineOpticalProductInfo> tpInvoiceLineOpticalProductInfos { get; set; }
        public virtual ICollection<trOrderOpticalProductLine> trOrderOpticalProductLines { get; set; }
    }
}
