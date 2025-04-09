using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdInsuranceAgency")]
    public partial class cdInsuranceAgency
    {
        public cdInsuranceAgency()
        {
            cdInsuranceAgencyDescs = new HashSet<cdInsuranceAgencyDesc>();
            prInsuranceAgencyContributions = new HashSet<prInsuranceAgencyContribution>();
            tpInvoiceHeaderExtensions = new HashSet<tpInvoiceHeaderExtension>();
            tpInvoiceLineOpticalProductInfos = new HashSet<tpInvoiceLineOpticalProductInfo>();
            tpInvoiceOpticalContributions = new HashSet<tpInvoiceOpticalContribution>();
            tpOrderHeaderExtensions = new HashSet<tpOrderHeaderExtension>();
            trOrderOpticalProducts = new HashSet<trOrderOpticalProduct>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string InsuranceAgencyCode { get; set; }

        [Required]
        public bool IsSGKAgency { get; set; }

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

        public virtual ICollection<cdInsuranceAgencyDesc> cdInsuranceAgencyDescs { get; set; }
        public virtual ICollection<prInsuranceAgencyContribution> prInsuranceAgencyContributions { get; set; }
        public virtual ICollection<tpInvoiceHeaderExtension> tpInvoiceHeaderExtensions { get; set; }
        public virtual ICollection<tpInvoiceLineOpticalProductInfo> tpInvoiceLineOpticalProductInfos { get; set; }
        public virtual ICollection<tpInvoiceOpticalContribution> tpInvoiceOpticalContributions { get; set; }
        public virtual ICollection<tpOrderHeaderExtension> tpOrderHeaderExtensions { get; set; }
        public virtual ICollection<trOrderOpticalProduct> trOrderOpticalProducts { get; set; }
    }
}
