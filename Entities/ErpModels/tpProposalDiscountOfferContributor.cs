using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpProposalDiscountOfferContributor")]
    public partial class tpProposalDiscountOfferContributor
    {
        public tpProposalDiscountOfferContributor()
        {
        }

        [Key]
        [Required]
        public Guid ProposalDiscountOfferContributorID { get; set; }

        [Required]
        public Guid ProposalHeaderID { get; set; }

        public Guid? ProposalLineID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountOfferCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal DiscountAmount { get; set; }

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
        public virtual trProposalLine trProposalLine { get; set; }
        public virtual trProposalHeader trProposalHeader { get; set; }

    }
}
