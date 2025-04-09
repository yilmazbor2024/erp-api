using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prEmployeeWorkplaceInformation")]
    public partial class prEmployeeWorkplaceInformation
    {
        public prEmployeeWorkplaceInformation()
        {
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkPlaceCode { get; set; }

        [Key]
        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobDepartmentCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SubJobDepartmentCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkForceCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MainJobTitleCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobTitleCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BusinessGroupCode { get; set; }

        [Required]
        public byte Level { get; set; }

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
        public virtual cdBusinessGroup cdBusinessGroup { get; set; }
        public virtual cdMainJobTitle cdMainJobTitle { get; set; }
        public virtual cdWorkForce cdWorkForce { get; set; }
        public virtual cdSubJobDepartment cdSubJobDepartment { get; set; }
        public virtual cdWorkPlace cdWorkPlace { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual cdJobTitle cdJobTitle { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }

    }
}
