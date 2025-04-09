using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsConfirmationStatus")]
    public partial class bsConfirmationStatus
    {
        public bsConfirmationStatus()
        {
            bsConfirmationStatusDescs = new HashSet<bsConfirmationStatusDesc>();
            tpProposalLineConfirmationStatuss = new HashSet<tpProposalLineConfirmationStatus>();
            tpPurchaseRequisitionProposals = new HashSet<tpPurchaseRequisitionProposal>();
        }

        [Key]
        [Required]
        public byte ConfirmationStatusCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsConfirmationStatusDesc> bsConfirmationStatusDescs { get; set; }
        public virtual ICollection<tpProposalLineConfirmationStatus> tpProposalLineConfirmationStatuss { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposal> tpPurchaseRequisitionProposals { get; set; }
    }
}
