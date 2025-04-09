using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdResignation")]
    public partial class cdResignation
    {
        public cdResignation()
        {
            cdResignationDescs = new HashSet<cdResignationDesc>();
            hrEmployeePayrollProfiles = new HashSet<hrEmployeePayrollProfile>();
            prEmployeePrevJobs = new HashSet<prEmployeePrevJob>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ResignationCode { get; set; }

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

        public virtual ICollection<cdResignationDesc> cdResignationDescs { get; set; }
        public virtual ICollection<hrEmployeePayrollProfile> hrEmployeePayrollProfiles { get; set; }
        public virtual ICollection<prEmployeePrevJob> prEmployeePrevJobs { get; set; }
    }
}
