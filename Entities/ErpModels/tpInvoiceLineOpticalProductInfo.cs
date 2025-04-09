using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceLineOpticalProductInfo")]
    public partial class tpInvoiceLineOpticalProductInfo
    {
        public tpInvoiceLineOpticalProductInfo()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceLineID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string InsuranceAgencyCode { get; set; }

        [Required]
        public byte MedulaDeclarationStatus { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OpticalSutCode { get; set; }

        [Required]
        public double ContributionAmount { get; set; }

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
        public virtual trInvoiceLine trInvoiceLine { get; set; }
        public virtual cdInsuranceAgency cdInsuranceAgency { get; set; }
        public virtual cdOpticalSut cdOpticalSut { get; set; }

    }
}
