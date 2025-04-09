using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdGLAccClass")]
    public partial class cdGLAccClass
    {
        public cdGLAccClass()
        {
            cdGLAccClassDescs = new HashSet<cdGLAccClassDesc>();
            cdGLAccGroups = new HashSet<cdGLAccGroup>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccClassCode { get; set; }

        [Required]
        public bool IsAsset { get; set; }

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

        public virtual ICollection<cdGLAccClassDesc> cdGLAccClassDescs { get; set; }
        public virtual ICollection<cdGLAccGroup> cdGLAccGroups { get; set; }
    }
}
