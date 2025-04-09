using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfSocialInsuranceRates")]
    public partial class dfSocialInsuranceRates
    {
        public dfSocialInsuranceRates()
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
        [Required]
        public byte EmployeeSocialInsuranceStatusCode { get; set; }

        [Required]
        public float UnemploymentEmployeeRate { get; set; }

        [Required]
        public float UnemploymentEmployerRate { get; set; }

        [Required]
        public float PensionEmployeeRate { get; set; }

        [Required]
        public float PensionEmployerRate { get; set; }

        [Required]
        public float HealthEmployeeRate { get; set; }

        [Required]
        public float HealthEmployerRate { get; set; }

        [Required]
        public float SGDPEmployeeRate { get; set; }

        [Required]
        public float SGDPEmployerRate { get; set; }

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
        public virtual cdEmployeeSocialInsuranceStatus cdEmployeeSocialInsuranceStatus { get; set; }
        public virtual cdCompany cdCompany { get; set; }

    }
}
