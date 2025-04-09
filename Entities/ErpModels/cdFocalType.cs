using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdFocalType")]
    public partial class cdFocalType
    {
        public cdFocalType()
        {
            cdFocalTypeDescs = new HashSet<cdFocalTypeDesc>();
            prProductLensPropertiess = new HashSet<prProductLensProperties>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string FocalTypeCode { get; set; }

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

        public virtual ICollection<cdFocalTypeDesc> cdFocalTypeDescs { get; set; }
        public virtual ICollection<prProductLensProperties> prProductLensPropertiess { get; set; }
    }
}
