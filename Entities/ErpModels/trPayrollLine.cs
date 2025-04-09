using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPayrollLine")]
    public partial class trPayrollLine
    {
        public trPayrollLine()
        {
            trPayrollLineDeductions = new HashSet<trPayrollLineDeduction>();
            trPayrollLineGarnishments = new HashSet<trPayrollLineGarnishment>();
            trPayrollLineTallys = new HashSet<trPayrollLineTally>();
            trPayrollTerminationSeveranceDetails = new HashSet<trPayrollTerminationSeveranceDetail>();
        }

        [Key]
        [Required]
        public Guid PayrollLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobDepartmentCode { get; set; }

        [Required]
        public byte PremiumDay { get; set; }

        [Required]
        public byte MissingWorkDay { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MissingWorkReasonCode { get; set; }

        [Required]
        public decimal OldRoundingDiff { get; set; }

        [Required]
        public decimal RoundingDiff { get; set; }

        [Required]
        public decimal NetAmount { get; set; }

        [Required]
        public bool IsAccrualDone { get; set; }

        [Required]
        public bool IsWorkedWithReport { get; set; }

        [Required]
        public Guid PayrollHeaderID { get; set; }

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
        public virtual trPayrollHeader trPayrollHeader { get; set; }
        public virtual cdMissingWorkReason cdMissingWorkReason { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<trPayrollLineDeduction> trPayrollLineDeductions { get; set; }
        public virtual ICollection<trPayrollLineGarnishment> trPayrollLineGarnishments { get; set; }
        public virtual ICollection<trPayrollLineTally> trPayrollLineTallys { get; set; }
        public virtual ICollection<trPayrollTerminationSeveranceDetail> trPayrollTerminationSeveranceDetails { get; set; }
    }
}
