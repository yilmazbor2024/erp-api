using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdEmployeeTaxStatus")]
    public partial class cdEmployeeTaxStatus
    {
        public cdEmployeeTaxStatus()
        {
            cdEmployeeTaxStatusDescs = new HashSet<cdEmployeeTaxStatusDesc>();
            dfIncomeTaxReliefs = new HashSet<dfIncomeTaxRelief>();
            hrEmployeePayrollProfiles = new HashSet<hrEmployeePayrollProfile>();
        }

        [Key]
        [Required]
        public byte EmployeeTaxStatusCode { get; set; }

        [Required]
        public bool ExemptFromIncomeTax { get; set; }

        [Required]
        public bool ExemptFromStampDuty { get; set; }

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

        public virtual ICollection<cdEmployeeTaxStatusDesc> cdEmployeeTaxStatusDescs { get; set; }
        public virtual ICollection<dfIncomeTaxRelief> dfIncomeTaxReliefs { get; set; }
        public virtual ICollection<hrEmployeePayrollProfile> hrEmployeePayrollProfiles { get; set; }
    }
}
