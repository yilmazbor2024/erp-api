using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdRequisition")]
    public partial class cdRequisition
    {
        public cdRequisition()
        {
            cdProposalConfirmationLimits = new HashSet<cdProposalConfirmationLimit>();
            cdProposalConfirmationRules = new HashSet<cdProposalConfirmationRule>();
            cdRequisitionConfirmationLimits = new HashSet<cdRequisitionConfirmationLimit>();
            cdRequisitionConfirmationRules = new HashSet<cdRequisitionConfirmationRule>();
            cdRequisitionDescs = new HashSet<cdRequisitionDesc>();
            prItemRequisitions = new HashSet<prItemRequisition>();
            prPurchasingAgentAvailableRequisitions = new HashSet<prPurchasingAgentAvailableRequisition>();
            prRequisitionAttributes = new HashSet<prRequisitionAttribute>();
            prRequisitionCurrAccs = new HashSet<prRequisitionCurrAcc>();
            prRequisitionLimits = new HashSet<prRequisitionLimit>();
            prTechnicalResponsibleAvailableRequisitions = new HashSet<prTechnicalResponsibleAvailableRequisition>();
            trPurchaseRequisitionLines = new HashSet<trPurchaseRequisitionLine>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RequisitionCode { get; set; }

        public byte? ItemTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RequisitionTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AveragePriceCurrencyCode { get; set; }

        [Required]
        public decimal AveragePrice { get; set; }

        [Required]
        public bool IsProposalRequired { get; set; }

        [Required]
        public byte MinProposalCount { get; set; }

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
        public virtual bsItemType bsItemType { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdRequisitionType cdRequisitionType { get; set; }

        public virtual ICollection<cdProposalConfirmationLimit> cdProposalConfirmationLimits { get; set; }
        public virtual ICollection<cdProposalConfirmationRule> cdProposalConfirmationRules { get; set; }
        public virtual ICollection<cdRequisitionConfirmationLimit> cdRequisitionConfirmationLimits { get; set; }
        public virtual ICollection<cdRequisitionConfirmationRule> cdRequisitionConfirmationRules { get; set; }
        public virtual ICollection<cdRequisitionDesc> cdRequisitionDescs { get; set; }
        public virtual ICollection<prItemRequisition> prItemRequisitions { get; set; }
        public virtual ICollection<prPurchasingAgentAvailableRequisition> prPurchasingAgentAvailableRequisitions { get; set; }
        public virtual ICollection<prRequisitionAttribute> prRequisitionAttributes { get; set; }
        public virtual ICollection<prRequisitionCurrAcc> prRequisitionCurrAccs { get; set; }
        public virtual ICollection<prRequisitionLimit> prRequisitionLimits { get; set; }
        public virtual ICollection<prTechnicalResponsibleAvailableRequisition> prTechnicalResponsibleAvailableRequisitions { get; set; }
        public virtual ICollection<trPurchaseRequisitionLine> trPurchaseRequisitionLines { get; set; }
    }
}
