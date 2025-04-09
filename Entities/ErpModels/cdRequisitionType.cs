using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdRequisitionType")]
    public partial class cdRequisitionType
    {
        public cdRequisitionType()
        {
            cdProposalConfirmationLimits = new HashSet<cdProposalConfirmationLimit>();
            cdProposalConfirmationRules = new HashSet<cdProposalConfirmationRule>();
            cdRequisitions = new HashSet<cdRequisition>();
            cdRequisitionConfirmationLimits = new HashSet<cdRequisitionConfirmationLimit>();
            cdRequisitionConfirmationRules = new HashSet<cdRequisitionConfirmationRule>();
            cdRequisitionTypeDescs = new HashSet<cdRequisitionTypeDesc>();
            prPurchasingAgentAvailableRequisitions = new HashSet<prPurchasingAgentAvailableRequisition>();
            prRequisitionLimits = new HashSet<prRequisitionLimit>();
            prTechnicalResponsibleAvailableRequisitions = new HashSet<prTechnicalResponsibleAvailableRequisition>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RequisitionTypeCode { get; set; }

        public byte? ItemTypeCode { get; set; }

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

        public virtual ICollection<cdProposalConfirmationLimit> cdProposalConfirmationLimits { get; set; }
        public virtual ICollection<cdProposalConfirmationRule> cdProposalConfirmationRules { get; set; }
        public virtual ICollection<cdRequisition> cdRequisitions { get; set; }
        public virtual ICollection<cdRequisitionConfirmationLimit> cdRequisitionConfirmationLimits { get; set; }
        public virtual ICollection<cdRequisitionConfirmationRule> cdRequisitionConfirmationRules { get; set; }
        public virtual ICollection<cdRequisitionTypeDesc> cdRequisitionTypeDescs { get; set; }
        public virtual ICollection<prPurchasingAgentAvailableRequisition> prPurchasingAgentAvailableRequisitions { get; set; }
        public virtual ICollection<prRequisitionLimit> prRequisitionLimits { get; set; }
        public virtual ICollection<prTechnicalResponsibleAvailableRequisition> prTechnicalResponsibleAvailableRequisitions { get; set; }
    }
}
