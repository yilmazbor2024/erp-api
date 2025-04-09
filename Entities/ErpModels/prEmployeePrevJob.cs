using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prEmployeePrevJob")]
    public partial class prEmployeePrevJob
    {
        public prEmployeePrevJob()
        {
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Key]
        [Required]
        public byte PrevJobTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CompanyName { get; set; }

        [Required]
        public DateTime JobStartDate { get; set; }

        [Required]
        public DateTime JobEndDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string StateCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CityCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IndustryCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobTitleCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ContactPerson { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Telephone { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Email { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string WebAddress { get; set; }

        [Required]
        public int NoOfEmployeesResponsible { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [Required]
        public int NoOfEmployees { get; set; }

        [Required]
        public int PeriodOfExperience { get; set; }

        [Required]
        public decimal MonthlyIncome { get; set; }

        [Required]
        public int AnnualBonus { get; set; }

        [Required]
        public decimal AnnualIncome { get; set; }

        [Required]
        public int AnnualSalaryRise { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ResignationCode { get; set; }

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
        public virtual cdCity cdCity { get; set; }
        public virtual cdState cdState { get; set; }
        public virtual cdCountry cdCountry { get; set; }
        public virtual cdJobType cdJobType { get; set; }
        public virtual cdPrevJobType cdPrevJobType { get; set; }
        public virtual cdIndustry cdIndustry { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual cdJobTitle cdJobTitle { get; set; }
        public virtual cdResignation cdResignation { get; set; }

    }
}
