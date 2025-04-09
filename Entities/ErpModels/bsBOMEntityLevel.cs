using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBOMEntityLevel")]
    public partial class bsBOMEntityLevel
    {
        public bsBOMEntityLevel()
        {
            bsBOMEntityLevelDescs = new HashSet<bsBOMEntityLevelDesc>();
        }

        [Key]
        [Required]
        public byte BOMEntityLevelCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsBOMEntityLevelDesc> bsBOMEntityLevelDescs { get; set; }
    }
}
