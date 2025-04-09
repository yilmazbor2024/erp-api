using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trProposalHeader")]
    public partial class trProposalHeader
    {
        public trProposalHeader()
        {
            tpProposalATAttributes = new HashSet<tpProposalATAttribute>();
            tpProposalDiscountOffers = new HashSet<tpProposalDiscountOffer>();
            tpProposalDiscountOfferContributors = new HashSet<tpProposalDiscountOfferContributor>();
            tpProposalFTAttributes = new HashSet<tpProposalFTAttribute>();
            trProposalLines = new HashSet<trProposalLine>();
            trProposalLineSums = new HashSet<trProposalLineSum>();
            trProposalLineSumDetails = new HashSet<trProposalLineSumDetail>();
        }

        [Key]
        [Required]
        public Guid ProposalHeaderID { get; set; }

        [Required]
        public byte TransTypeCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object ProposalNumber { get; set; }

        [Required]
        public DateTime ProposalDate { get; set; }

        [Required]
        public TimeSpan ProposalTime { get; set; }

        [Required]
        public DateTime ExpiredDate { get; set; }

        [Required]
        public short PaymentTerm { get; set; }

        [Required]
        public DateTime AverageDueDate { get; set; }

        public string Description { get; set; }

        public string InternalDescription { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ShipmentMethodCode { get; set; }

        public Guid? ShippingPostalAddressID { get; set; }

        public Guid? BillingPostalAddressID { get; set; }

        [Required]
        public byte TaxTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WithHoldingTaxTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DOVCode { get; set; }

        [Required]
        public short TaxExemptionCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CompanyName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxOfficeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TaxNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public double TDisRate1 { get; set; }

        [Required]
        public double TDisRate2 { get; set; }

        [Required]
        public double TDisRate3 { get; set; }

        [Required]
        public double TDisRate4 { get; set; }

        [Required]
        public double TDisRate5 { get; set; }

        [Required]
        public double StoppageRate { get; set; }

        [Required]
        public decimal TaxRefund { get; set; }

        [Required]
        public object ImportFileNumber { get; set; }

        [Required]
        public object ExportFileNumber { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IncotermCode1 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IncotermCode2 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PaymentMethodCode { get; set; }

        public bool? Canceled { get; set; }

        [Required]
        public DateTime CancelDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CancelReason { get; set; }

        [Required]
        public bool IsInclutedVat { get; set; }

        [Required]
        public bool IsCreditSale { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedUserName { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual bsWithHoldingTaxType bsWithHoldingTaxType { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual bsIncoterm bsIncoterm { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdShipmentMethod cdShipmentMethod { get; set; }
        public virtual bsTransType bsTransType { get; set; }
        public virtual cdDOV cdDOV { get; set; }
        public virtual cdPaymentMethod cdPaymentMethod { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual prCurrAccPostalAddress prCurrAccPostalAddress { get; set; }
        public virtual cdTaxOffice cdTaxOffice { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsTaxType bsTaxType { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual bsTaxExemption bsTaxExemption { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpProposalATAttribute> tpProposalATAttributes { get; set; }
        public virtual ICollection<tpProposalDiscountOffer> tpProposalDiscountOffers { get; set; }
        public virtual ICollection<tpProposalDiscountOfferContributor> tpProposalDiscountOfferContributors { get; set; }
        public virtual ICollection<tpProposalFTAttribute> tpProposalFTAttributes { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
        public virtual ICollection<trProposalLineSum> trProposalLineSums { get; set; }
        public virtual ICollection<trProposalLineSumDetail> trProposalLineSumDetails { get; set; }
    }
}
