using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBaseMaterial")]
    public partial class cdBaseMaterial
    {
        public cdBaseMaterial()
        {
            cdBaseMaterialDescs = new HashSet<cdBaseMaterialDesc>();
            prProductFramePropertiess = new HashSet<prProductFrameProperties>();
            prProductLensPropertiess = new HashSet<prProductLensProperties>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string BaseMaterialCode { get; set; }

        [Key]
        [Required]
        public byte ProductTypeCode { get; set; }

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
        public virtual bsProductType bsProductType { get; set; }

        public virtual ICollection<cdBaseMaterialDesc> cdBaseMaterialDescs { get; set; }
        public virtual ICollection<prProductFrameProperties> prProductFramePropertiess { get; set; }
        public virtual ICollection<prProductLensProperties> prProductLensPropertiess { get; set; }
    }
}
