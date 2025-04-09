using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpProposalDiscountOffer")]
    public partial class tpProposalDiscountOffer
    {
        public tpProposalDiscountOffer()
        {
        }

        [Key]
        [Required]
        public Guid ProposalDiscountOfferID { get; set; }

        [Required]
        public Guid ProposalHeaderID { get; set; }

        public Guid? ProposalLineID { get; set; }

        [Required]
        public bool IsEarned { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountOfferCode { get; set; }

        [Required]
        public decimal DiscountAmount { get; set; }

        [Required]
        public double DiscountRate { get; set; }

        [Required]
        public byte PaymentTypeCode { get; set; }

        [Required]
        public bool UsedAsPayment { get; set; }

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
        public virtual trProposalHeader trProposalHeader { get; set; }
        public virtual trProposalLine trProposalLine { get; set; }

    }
}
