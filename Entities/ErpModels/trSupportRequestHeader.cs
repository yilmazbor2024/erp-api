using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trSupportRequestHeader")]
    public partial class trSupportRequestHeader
    {
        public trSupportRequestHeader()
        {
            tpSupportRequestConfirmations = new HashSet<tpSupportRequestConfirmation>();
            tpSupportRequestDecisionLetters = new HashSet<tpSupportRequestDecisionLetter>();
            tpSupportRequestInformations = new HashSet<tpSupportRequestInformation>();
            tpSupportResolves = new HashSet<tpSupportResolve>();
            tpSupportStatusHistorys = new HashSet<tpSupportStatusHistory>();
            trInnerLines = new HashSet<trInnerLine>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trOrderLines = new HashSet<trOrderLine>();
            trShipmentLines = new HashSet<trShipmentLine>();
            trSupportRequestLines = new HashSet<trSupportRequestLine>();
            trSupportRequestSurveys = new HashSet<trSupportRequestSurvey>();
        }

        [Key]
        [Required]
        public Guid SupportRequestHeaderID { get; set; }

        [Required]
        public object SupportRequestNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public byte SupportTypeCode { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        public TimeSpan RequestTime { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public TimeSpan DeliveryTime { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        [Required]
        public bool SendResultsBySMS { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PhoneNumberForSMS { get; set; }

        [Required]
        public bool SendResultsByEMail { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMailAddress { get; set; }

        [Required]
        public byte ProductTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SerialNumber { get; set; }

        [Required]
        public byte PriorityCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ShipmentMethodCode { get; set; }

        public Guid? ShippingPostalAddressID { get; set; }

        public Guid? BillingPostalAddressID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryCompanyCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }
 
        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public short POSTerminalID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public byte CanChangeProduct { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        [Required]
        public byte ConfirmationTypeCode { get; set; }

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

        public Guid? ApplicationLineID { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual cdDeliveryCompany cdDeliveryCompany { get; set; }
        public virtual cdSalesperson cdSalesperson { get; set; }
        public virtual cdShipmentMethod cdShipmentMethod { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual prCurrAccPostalAddress prCurrAccPostalAddress { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual bsSupportType bsSupportType { get; set; }

        public virtual ICollection<tpSupportRequestConfirmation> tpSupportRequestConfirmations { get; set; }
        public virtual ICollection<tpSupportRequestDecisionLetter> tpSupportRequestDecisionLetters { get; set; }
        public virtual ICollection<tpSupportRequestInformation> tpSupportRequestInformations { get; set; }
        public virtual ICollection<tpSupportResolve> tpSupportResolves { get; set; }
        public virtual ICollection<tpSupportStatusHistory> tpSupportStatusHistorys { get; set; }
        public virtual ICollection<trInnerLine> trInnerLines { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
        public virtual ICollection<trSupportRequestLine> trSupportRequestLines { get; set; }
        public virtual ICollection<trSupportRequestSurvey> trSupportRequestSurveys { get; set; }
    }
}
