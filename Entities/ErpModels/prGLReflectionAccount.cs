using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prGLReflectionAccount")]
    public partial class prGLReflectionAccount
    {
        public prGLReflectionAccount()
        {
        }

        [Key]
        [Required]
        public Guid GLReflectionID { get; set; }

        [Required]
        public object CompanyCode { get; set; }
 
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string GLReflectionCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExpenseGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ReflectionGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string IncomeGLAccCode { get; set; }

        [Required]
        public float Rate { get; set; }

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

        // Navigation Properties
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual cdGLReflection cdGLReflection { get; set; }

    }
}
