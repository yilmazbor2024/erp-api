using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsEyeGlassSutType")]
    public partial class bsEyeGlassSutType
    {
        public bsEyeGlassSutType()
        {
            bsEyeGlassSutTypeDescs = new HashSet<bsEyeGlassSutTypeDesc>();
            cdOpticalSuts = new HashSet<cdOpticalSut>();
            prProductLensPropertiess = new HashSet<prProductLensProperties>();
        }

        [Key]
        [Required]
        public byte EyeGlassSutTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsEyeGlassSutTypeDesc> bsEyeGlassSutTypeDescs { get; set; }
        public virtual ICollection<cdOpticalSut> cdOpticalSuts { get; set; }
        public virtual ICollection<prProductLensProperties> prProductLensPropertiess { get; set; }
    }
}
