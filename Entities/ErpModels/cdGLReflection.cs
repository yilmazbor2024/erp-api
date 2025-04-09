using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdGLReflection")]
    public partial class cdGLReflection
    {
        public cdGLReflection()
        {
            cdGLReflectionDescs = new HashSet<cdGLReflectionDesc>();
            prGLReflectionAccounts = new HashSet<prGLReflectionAccount>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string GLReflectionCode { get; set; }

        [Required]
        public bool IsReflectionDocCurrency { get; set; }

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

        public virtual ICollection<cdGLReflectionDesc> cdGLReflectionDescs { get; set; }
        public virtual ICollection<prGLReflectionAccount> prGLReflectionAccounts { get; set; }
    }
}
