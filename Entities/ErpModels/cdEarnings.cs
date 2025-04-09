using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdEarnings")]
    public partial class cdEarnings
    {
        public cdEarnings()
        {
            cdEarningsDescs = new HashSet<cdEarningsDesc>();
            dfCompanyEarningsDefaults = new HashSet<dfCompanyEarningsDefault>();
            hrEmployeeMonthlySumDetails = new HashSet<hrEmployeeMonthlySumDetail>();
            trPayrollLineTallys = new HashSet<trPayrollLineTally>();
            trPayrollTerminationSeveranceDetails = new HashSet<trPayrollTerminationSeveranceDetail>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EarningsCode { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsDuePay { get; set; }

        [Required]
        public bool UseForTerminationPay { get; set; }

        [Required]
        public bool UseForSeverancePay { get; set; }

        [Required]
        public bool UseForCompulsoryPensionInsurance { get; set; }

        [Required]
        public bool IsBenefitsInKind { get; set; }

        [Required]
        public float GarnishmentRate { get; set; }

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

        public virtual ICollection<cdEarningsDesc> cdEarningsDescs { get; set; }
        public virtual ICollection<dfCompanyEarningsDefault> dfCompanyEarningsDefaults { get; set; }
        public virtual ICollection<hrEmployeeMonthlySumDetail> hrEmployeeMonthlySumDetails { get; set; }
        public virtual ICollection<trPayrollLineTally> trPayrollLineTallys { get; set; }
        public virtual ICollection<trPayrollTerminationSeveranceDetail> trPayrollTerminationSeveranceDetails { get; set; }
    }
}
