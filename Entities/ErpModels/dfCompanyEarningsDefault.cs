using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfCompanyEarningsDefault")]
    public partial class dfCompanyEarningsDefault
    {
        public dfCompanyEarningsDefault()
        {
            dfCompanyEarningsMonthlys = new HashSet<dfCompanyEarningsMonthly>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EarningsCode { get; set; }

        [Required]
        public bool IsDailyEarnings { get; set; }

        [Required]
        public bool ExemptFromIncomeTax { get; set; }

        [Required]
        public bool ExemptFromInsurance { get; set; }

        [Required]
        public bool ExemptFromStampDuty { get; set; }

        [Required]
        public bool IncludePrivateInsuranceBase { get; set; }

        [Required]
        public bool UseRDHours { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        [Required]
        public DateTime ClosedDate { get; set; }

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

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdEarnings cdEarnings { get; set; }

        public virtual ICollection<dfCompanyEarningsMonthly> dfCompanyEarningsMonthlys { get; set; }
    }
}
