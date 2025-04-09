using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prEmployeeLeaveDay")]
    public partial class prEmployeeLeaveDay
    {
        public prEmployeeLeaveDay()
        {
        }

        [Key]
        [Required]
        public Guid EmployeeLeaveID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public DateTime LeaveDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LeaveTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Description { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MissingWorkReasonCode { get; set; }

        [Required]
        public bool IsWithoutPay { get; set; }

        [Required]
        public bool IsPlanned { get; set; }

        [Required]
        public bool IsUsed { get; set; }

        public Guid? EmployeeLeaveRequestID { get; set; }

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
        public virtual prEmployeeLeaveRequest prEmployeeLeaveRequest { get; set; }
        public virtual cdMissingWorkReason cdMissingWorkReason { get; set; }
        public virtual cdLeaveType cdLeaveType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
