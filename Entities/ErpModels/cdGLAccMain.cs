using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdGLAccMain")]
    public partial class cdGLAccMain
    {
        public cdGLAccMain()
        {
            cdGLAccs = new HashSet<cdGLAcc>();
            cdGLAccMainDescs = new HashSet<cdGLAccMainDesc>();
            cdGLAccSubs = new HashSet<cdGLAccSub>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccMainCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccGroupCode { get; set; }

        [Required]
        public bool IsCreditAcc { get; set; }

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
        public virtual cdGLAccGroup cdGLAccGroup { get; set; }

        public virtual ICollection<cdGLAcc> cdGLAccs { get; set; }
        public virtual ICollection<cdGLAccMainDesc> cdGLAccMainDescs { get; set; }
        public virtual ICollection<cdGLAccSub> cdGLAccSubs { get; set; }
    }
}
