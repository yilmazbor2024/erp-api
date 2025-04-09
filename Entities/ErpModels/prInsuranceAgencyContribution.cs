using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prInsuranceAgencyContribution")]
    public partial class prInsuranceAgencyContribution
    {
        public prInsuranceAgencyContribution()
        {
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string InsuranceAgencyCode { get; set; }

        [Key]
        [Required]
        public byte ProductTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal ContributionAmount { get; set; }

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
        public virtual bsProductType bsProductType { get; set; }
        public virtual cdInsuranceAgency cdInsuranceAgency { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

    }
}
