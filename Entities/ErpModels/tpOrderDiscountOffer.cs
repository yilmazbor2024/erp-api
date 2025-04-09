using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderDiscountOffer")]
    public partial class tpOrderDiscountOffer
    {
        public tpOrderDiscountOffer()
        {
        }

        [Key]
        [Required]
        public Guid OrderDiscountOfferID { get; set; }

        [Required]
        public Guid OrderHeaderID { get; set; }

        public Guid? OrderLineID { get; set; }

        public Guid? OrderCancelDetailHeaderID { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountVoucherTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SerialNumber { get; set; }

        [Required]
        public decimal EarnedAmount { get; set; }

        [Required]
        public decimal UsedAmount { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountPointTypeCode { get; set; }

        public Guid? CustomerDiscountPointID { get; set; }

        [Required]
        public decimal EarnedPoint { get; set; }

        [Required]
        public decimal UsedPoint { get; set; }

        [Required]
        public bool IsBooster { get; set; }

        [Required]
        public bool UsedAsPayment { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Password { get; set; }

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
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual trOrderHeader trOrderHeader { get; set; }

    }
}
