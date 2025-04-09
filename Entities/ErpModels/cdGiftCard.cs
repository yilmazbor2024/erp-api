using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdGiftCard")]
    public partial class cdGiftCard
    {
        public cdGiftCard()
        {
            prGiftCardCharges = new HashSet<prGiftCardCharge>();
            trGiftCardPaymentLines = new HashSet<trGiftCardPaymentLine>();
            trInnerLineGiftCards = new HashSet<trInnerLineGiftCard>();
            trInvoiceLineGiftCards = new HashSet<trInvoiceLineGiftCard>();
            trShipmentLineGiftCards = new HashSet<trShipmentLineGiftCard>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SerialNumber { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public DateTime FirstValidDate { get; set; }

        [Required]
        public int AfterSaleAvailableDay { get; set; }

        [Required]
        public DateTime LastValidDate { get; set; }

        [Required]
        public bool IsDisposable { get; set; }

        [Required]
        public bool CannotReturn { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal UsedAmount { get; set; }

        [Required]
        public decimal MinSaleAmount { get; set; }

        [Required]
        public decimal MaxSaleAmount { get; set; }

        [Required]
        public double SaleMultiplesOfValue { get; set; }

        [Required]
        public bool UseSMSVerificationOnUse { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string VerifiedPhoneNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MessageResponseID { get; set; }

        [Required]
        public bool IgnoreSMSVerificationOnUse { get; set; }

        public Guid? InvoiceHeaderID { get; set; }

        public Guid? OrderHeaderID { get; set; }

        [Required]
        public bool IsBearerVoucher { get; set; }

        [Required]
        public bool IsAmountToBeEnteredOnSale { get; set; }

        [Required]
        public bool IsSold { get; set; }

        [Required]
        public bool IsUsed { get; set; }

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
        public virtual cdItem cdItem { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

        public virtual ICollection<prGiftCardCharge> prGiftCardCharges { get; set; }
        public virtual ICollection<trGiftCardPaymentLine> trGiftCardPaymentLines { get; set; }
        public virtual ICollection<trInnerLineGiftCard> trInnerLineGiftCards { get; set; }
        public virtual ICollection<trInvoiceLineGiftCard> trInvoiceLineGiftCards { get; set; }
        public virtual ICollection<trShipmentLineGiftCard> trShipmentLineGiftCards { get; set; }
    }
}
