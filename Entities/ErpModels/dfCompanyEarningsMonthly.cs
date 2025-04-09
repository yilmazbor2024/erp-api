using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfCompanyEarningsMonthly")]
    public partial class dfCompanyEarningsMonthly
    {
        public dfCompanyEarningsMonthly()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

 
        [Key]
        [Required]
        public short ValidYear { get; set; }

        [Key]
        [Required]
        public byte ValidMonth { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EarningsCode { get; set; }

        [Required]
        public decimal EarningsAmount { get; set; }

        [Required]
        public decimal ExemptFromIncomeTaxAmount { get; set; }

        [Required]
        public bool IsExemptIncomeTaxDaily { get; set; }

        [Required]
        public decimal ExemptFromInsuranceAmount { get; set; }

        [Required]
        public bool IsExemptInsuranceDaily { get; set; }

        [Required]
        public decimal ExemptFromStampDutyAmount { get; set; }

        [Required]
        public bool IsExemptStampDutyDaily { get; set; }

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
        public virtual dfCompanyEarningsDefault dfCompanyEarningsDefault { get; set; }
        public virtual cdCompany cdCompany { get; set; }

    }
}
