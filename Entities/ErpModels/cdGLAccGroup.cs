using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdGLAccGroup")]
    public partial class cdGLAccGroup
    {
        public cdGLAccGroup()
        {
            cdGLAccGroupDescs = new HashSet<cdGLAccGroupDesc>();
            cdGLAccMains = new HashSet<cdGLAccMain>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccGroupCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccClassCode { get; set; }

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
        public virtual cdGLAccClass cdGLAccClass { get; set; }

        public virtual ICollection<cdGLAccGroupDesc> cdGLAccGroupDescs { get; set; }
        public virtual ICollection<cdGLAccMain> cdGLAccMains { get; set; }
    }
}
