using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("hrWageGarnishment")]
    public partial class hrWageGarnishment
    {
        public hrWageGarnishment()
        {
            trPayrollLineGarnishments = new HashSet<trPayrollLineGarnishment>();
        }

        [Key]
        [Required]
        public Guid WageGarnishmentID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public short SortOrder { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string WageGarnishmentTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ExecutionOfficeCode { get; set; }

        [Required]
        public bool EarningIncluded { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public bool ByRate { get; set; }

        [Required]
        public float Rate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string IBAN { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        [Required]
        public DateTime ClosedDate { get; set; }

        [Required]
        public bool NotPrivateGarnishment { get; set; }

        [Required]
        public bool AGIIncluded { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [Required]
        public bool CanNotCollectNextGarnishment { get; set; }

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
        public virtual hrEmployeePayrollProfile hrEmployeePayrollProfile { get; set; }
        public virtual cdExecutionOffice cdExecutionOffice { get; set; }
        public virtual cdWageGarnishmentType cdWageGarnishmentType { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

        public virtual ICollection<trPayrollLineGarnishment> trPayrollLineGarnishments { get; set; }
    }
}
