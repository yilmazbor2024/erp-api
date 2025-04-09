using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderDiscountOfferContributor")]
    public partial class tpOrderDiscountOfferContributor
    {
        public tpOrderDiscountOfferContributor()
        {
        }

        [Key]
        [Required]
        public Guid OrderDiscountOfferContributorID { get; set; }

        [Required]
        public Guid OrderHeaderID { get; set; }

        public Guid? OrderLineID { get; set; }

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
        public virtual trOrderHeader trOrderHeader { get; set; }
        public virtual trOrderLine trOrderLine { get; set; }

    }
}
