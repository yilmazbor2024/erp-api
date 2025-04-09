using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdColorGroup")]
    public partial class cdColorGroup
    {
        public cdColorGroup()
        {
            cdColorGroupDescs = new HashSet<cdColorGroupDesc>();
            prItemColorAttributess = new HashSet<prItemColorAttributes>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorGroupCode { get; set; }

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

        public virtual ICollection<cdColorGroupDesc> cdColorGroupDescs { get; set; }
        public virtual ICollection<prItemColorAttributes> prItemColorAttributess { get; set; }
    }
}
