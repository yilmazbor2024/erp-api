using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prEmployeeLeaveHour")]
    public partial class prEmployeeLeaveHour
    {
        public prEmployeeLeaveHour()
        {
        }

        [Key]
        [Required]
        public Guid EmployeeLeaveHourID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public DateTime LeaveDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public TimeSpan LeaveHour { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LeaveTypeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MissingWorkReasonCode { get; set; }

        [Required]
        public bool IsPlanned { get; set; }

        [Required]
        public bool IsUsed { get; set; }

        public Guid? EmployeeLeaveRequestHourID { get; set; }

        public Guid? EmployeeLeaveRequestHeaderID { get; set; }

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
        public virtual cdLeaveType cdLeaveType { get; set; }
        public virtual cdMissingWorkReason cdMissingWorkReason { get; set; }
        public virtual prEmployeeLeaveRequestHour prEmployeeLeaveRequestHour { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
