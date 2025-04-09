using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDueDateFormula")]
    public partial class cdDueDateFormula
    {
        public cdDueDateFormula()
        {
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdDueDateFormulaDescs = new HashSet<cdDueDateFormulaDesc>();
            cdPaymentPlans = new HashSet<cdPaymentPlan>();
            prProcessInfos = new HashSet<prProcessInfo>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DueDateFormulaCode { get; set; }

        [Required]
        public byte CurrentMonthSegment1 { get; set; }

        [Required]
        public short DelayDay1 { get; set; }

        [Required]
        public byte DueDay1 { get; set; }

        [Required]
        public byte DayCode1 { get; set; }

        [Required]
        public byte CurrentMonthSegment2 { get; set; }

        [Required]
        public short DelayDay2 { get; set; }

        [Required]
        public byte DueDay2 { get; set; }

        [Required]
        public byte DayCode2 { get; set; }

        [Required]
        public byte CurrentMonthSegment3 { get; set; }

        [Required]
        public short DelayDay3 { get; set; }

        [Required]
        public byte DueDay3 { get; set; }

        [Required]
        public byte DayCode3 { get; set; }

        [Required]
        public byte CurrentMonthSegment4 { get; set; }

        [Required]
        public short DelayDay4 { get; set; }

        [Required]
        public byte DueDay4 { get; set; }

        [Required]
        public byte DayCode4 { get; set; }

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

        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdDueDateFormulaDesc> cdDueDateFormulaDescs { get; set; }
        public virtual ICollection<cdPaymentPlan> cdPaymentPlans { get; set; }
        public virtual ICollection<prProcessInfo> prProcessInfos { get; set; }
    }
}
