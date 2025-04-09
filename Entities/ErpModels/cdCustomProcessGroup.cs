using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCustomProcessGroup")]
    public partial class cdCustomProcessGroup
    {
        public cdCustomProcessGroup()
        {
            cdCustomProcessGroupDescs = new HashSet<cdCustomProcessGroupDesc>();
            prCustomProcessGroupAtts = new HashSet<prCustomProcessGroupAtt>();
            prProductFramePropertiess = new HashSet<prProductFrameProperties>();
            prProductLensPropertiess = new HashSet<prProductLensProperties>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomProcessGroupCode { get; set; }

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

        public virtual ICollection<cdCustomProcessGroupDesc> cdCustomProcessGroupDescs { get; set; }
        public virtual ICollection<prCustomProcessGroupAtt> prCustomProcessGroupAtts { get; set; }
        public virtual ICollection<prProductFrameProperties> prProductFramePropertiess { get; set; }
        public virtual ICollection<prProductLensProperties> prProductLensPropertiess { get; set; }
    }
}
