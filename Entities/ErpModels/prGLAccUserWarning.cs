using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prGLAccUserWarning")]
    public partial class prGLAccUserWarning
    {
        public prGLAccUserWarning()
        {
        }

        [Key]
        [Required]
        public Guid GLAccUserWarningID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UserWarningCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object LineDescription { get; set; }

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

        // Navigation Properties
        public virtual cdUserWarning cdUserWarning { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }

    }
}
