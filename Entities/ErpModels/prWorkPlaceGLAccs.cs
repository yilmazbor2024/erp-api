using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prWorkPlaceGLAccs")]
    public partial class prWorkPlaceGLAccs
    {
        public prWorkPlaceGLAccs()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkPlaceCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobDepartmentCode { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string AccountCode { get; set; }

        [Key]
        [Required]
        public bool ForMinWage { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [Required]
        public bool EmployeeDetailedAccount { get; set; }

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
        public virtual cdWorkPlace cdWorkPlace { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }

    }
}
