using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLeaveType")]
    public partial class cdLeaveType
    {
        public cdLeaveType()
        {
            cdLeaveTypeDescs = new HashSet<cdLeaveTypeDesc>();
            prEmployeeLeaveDays = new HashSet<prEmployeeLeaveDay>();
            prEmployeeLeaveHours = new HashSet<prEmployeeLeaveHour>();
            prEmployeeLeaveRequests = new HashSet<prEmployeeLeaveRequest>();
            prEmployeeLeaveRequestHours = new HashSet<prEmployeeLeaveRequestHour>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LeaveTypeCode { get; set; }

        [Required]
        public bool IsWithoutPay { get; set; }

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

        public virtual ICollection<cdLeaveTypeDesc> cdLeaveTypeDescs { get; set; }
        public virtual ICollection<prEmployeeLeaveDay> prEmployeeLeaveDays { get; set; }
        public virtual ICollection<prEmployeeLeaveHour> prEmployeeLeaveHours { get; set; }
        public virtual ICollection<prEmployeeLeaveRequest> prEmployeeLeaveRequests { get; set; }
        public virtual ICollection<prEmployeeLeaveRequestHour> prEmployeeLeaveRequestHours { get; set; }
    }
}
