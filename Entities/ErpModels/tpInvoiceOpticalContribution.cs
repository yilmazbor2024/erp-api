using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceOpticalContribution")]
    public partial class tpInvoiceOpticalContribution
    {
        public tpInvoiceOpticalContribution()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceOpticalContributionID { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Required]
        public Guid InvoiceLineID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string InsuranceAgencyCode { get; set; }

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

        // Navigation Properties
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual cdInsuranceAgency cdInsuranceAgency { get; set; }
        public virtual trInvoiceLine trInvoiceLine { get; set; }

    }
}
