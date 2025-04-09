using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdGLAccSub")]
    public partial class cdGLAccSub
    {
        public cdGLAccSub()
        {
            cdGLAccs = new HashSet<cdGLAcc>();
            cdGLAccSubDescs = new HashSet<cdGLAccSubDesc>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccMainCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccSubCode1 { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccSubCode2 { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccSubCode3 { get; set; }

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
        public virtual cdGLAccMain cdGLAccMain { get; set; }
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<cdGLAcc> cdGLAccs { get; set; }
        public virtual ICollection<cdGLAccSubDesc> cdGLAccSubDescs { get; set; }
    }
}
