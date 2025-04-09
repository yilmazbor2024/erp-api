using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdTaxOffice")]
    public partial class cdTaxOffice
    {
        public cdTaxOffice()
        {
            bsTaxFreeRefundCompanys = new HashSet<bsTaxFreeRefundCompany>();
            cdCheques = new HashSet<cdCheque>();
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdGLAccs = new HashSet<cdGLAcc>();
            cdTaxOfficeDescs = new HashSet<cdTaxOfficeDesc>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            prCurrAccPostalAddresss = new HashSet<prCurrAccPostalAddress>();
            prSubCurrAccs = new HashSet<prSubCurrAcc>();
            tpPurchaseRequisitionProposals = new HashSet<tpPurchaseRequisitionProposal>();
            trExpenseSlipLines = new HashSet<trExpenseSlipLine>();
            trJournalLines = new HashSet<trJournalLine>();
            trProposalHeaders = new HashSet<trProposalHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxOfficeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CityCode { get; set; }

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

        // Navigation Properties
        public virtual cdCity cdCity { get; set; }

        public virtual ICollection<bsTaxFreeRefundCompany> bsTaxFreeRefundCompanys { get; set; }
        public virtual ICollection<cdCheque> cdCheques { get; set; }
        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdGLAcc> cdGLAccs { get; set; }
        public virtual ICollection<cdTaxOfficeDesc> cdTaxOfficeDescs { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<prCurrAccPostalAddress> prCurrAccPostalAddresss { get; set; }
        public virtual ICollection<prSubCurrAcc> prSubCurrAccs { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposal> tpPurchaseRequisitionProposals { get; set; }
        public virtual ICollection<trExpenseSlipLine> trExpenseSlipLines { get; set; }
        public virtual ICollection<trJournalLine> trJournalLines { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
    }
}
