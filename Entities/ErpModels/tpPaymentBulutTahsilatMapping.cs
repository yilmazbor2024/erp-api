using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPaymentBulutTahsilatMapping")]
    public partial class tpPaymentBulutTahsilatMapping
    {
        public tpPaymentBulutTahsilatMapping()
        {
        }

        [Key]
        [Required]
        public Guid CorrelationID { get; set; }

        public Guid? OrderHeaderID { get; set; }

        public Guid? PaymentHeaderID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string OrderGuidCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OrderDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TotalPriceWithVat { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Currency { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CreditCardNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string VBankPosCode { get; set; }

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

    }
}
