using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDeduction")]
    public partial class cdDeduction
    {
        public cdDeduction()
        {
            cdDeductionDescs = new HashSet<cdDeductionDesc>();
            dfCompanyDeductionDefaults = new HashSet<dfCompanyDeductionDefault>();
            dfPayrollDefaults = new HashSet<dfPayrollDefault>();
            trEmployeeDebits = new HashSet<trEmployeeDebit>();
            trPayrollLineDeductions = new HashSet<trPayrollLineDeduction>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeductionCode { get; set; }

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

        public virtual ICollection<cdDeductionDesc> cdDeductionDescs { get; set; }
        public virtual ICollection<dfCompanyDeductionDefault> dfCompanyDeductionDefaults { get; set; }
        public virtual ICollection<dfPayrollDefault> dfPayrollDefaults { get; set; }
        public virtual ICollection<trEmployeeDebit> trEmployeeDebits { get; set; }
        public virtual ICollection<trPayrollLineDeduction> trPayrollLineDeductions { get; set; }
    }
}
