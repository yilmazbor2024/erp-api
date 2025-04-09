using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdMissingWorkReason")]
    public partial class cdMissingWorkReason
    {
        public cdMissingWorkReason()
        {
            cdMissingWorkReasonDescs = new HashSet<cdMissingWorkReasonDesc>();
            hrEmployeeMonthlySums = new HashSet<hrEmployeeMonthlySum>();
            hrEmployeeMonthlySumDetails = new HashSet<hrEmployeeMonthlySumDetail>();
            prEmployeeLeaveDays = new HashSet<prEmployeeLeaveDay>();
            prEmployeeLeaveHours = new HashSet<prEmployeeLeaveHour>();
            trPayrollLines = new HashSet<trPayrollLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MissingWorkReasonCode { get; set; }

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

        public virtual ICollection<cdMissingWorkReasonDesc> cdMissingWorkReasonDescs { get; set; }
        public virtual ICollection<hrEmployeeMonthlySum> hrEmployeeMonthlySums { get; set; }
        public virtual ICollection<hrEmployeeMonthlySumDetail> hrEmployeeMonthlySumDetails { get; set; }
        public virtual ICollection<prEmployeeLeaveDay> prEmployeeLeaveDays { get; set; }
        public virtual ICollection<prEmployeeLeaveHour> prEmployeeLeaveHours { get; set; }
        public virtual ICollection<trPayrollLine> trPayrollLines { get; set; }
    }
}
