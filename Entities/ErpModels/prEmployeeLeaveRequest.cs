using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prEmployeeLeaveRequest")]
    public partial class prEmployeeLeaveRequest
    {
        public prEmployeeLeaveRequest()
        {
            prEmployeeLeaveDays = new HashSet<prEmployeeLeaveDay>();
        }

        [Key]
        [Required]
        public Guid EmployeeLeaveRequestID { get; set; }

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

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string RequestDescription { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ContactPerson { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ContactPhoneNumber { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Address { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DepartmentManagerCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string HRManagerCode { get; set; }

        [Required]
        public byte Status { get; set; }

        public DateTime? StatusUpdateDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string StatusUpdateUserName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string RejectReason { get; set; }

        [Required]
        public Guid EmployeeLeaveRequestHeaderID { get; set; }

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
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<prEmployeeLeaveDay> prEmployeeLeaveDays { get; set; }
    }
}
