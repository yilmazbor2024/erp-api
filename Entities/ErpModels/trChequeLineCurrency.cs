using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trChequeLineCurrency")]
    public partial class trChequeLineCurrency
    {
        public trChequeLineCurrency()
        {
        }

        [Key]
        [Required]
        public Guid ChequeLineID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RelationCurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal ProtestCharge { get; set; }

        [Required]
        public decimal TransferCharge { get; set; }

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
        public virtual trChequeLine trChequeLine { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

    }
}
