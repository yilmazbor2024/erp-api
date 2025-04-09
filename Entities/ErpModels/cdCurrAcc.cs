using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCurrAcc")]
    public partial class cdCurrAcc
    {
        public cdCurrAcc()
        {
            cdCurrAccDescs = new HashSet<cdCurrAccDesc>();
            prCustomerVendorAccounts = new HashSet<prCustomerVendorAccount>();
            prCurrAccDefaults = new HashSet<prCurrAccDefault>();
            prCurrAccPostalAddresss = new HashSet<prCurrAccPostalAddress>();
        }

        [Key]
        [Column(Order = 0)]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "Char30")]
        [StringLength(30)]
        public string CurrAccCode { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "ComNum")]
        public string CompanyCode { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "Office")]
        public string OfficeCode { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TitleCode { get; set; }

        [Required]
        [StringLength(60)]
        [Column(TypeName = "Char60")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60)]
        [Column(TypeName = "Char60")]
        public string Patronym { get; set; }

        [Required]
        [StringLength(60)]
        [Column(TypeName = "Char60")]
        public string LastName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FirstLastName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; set; }

        [Required]
        public bool IsIndividualAcc { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TaxOfficeCode { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxNumber { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IdentityNum { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MersisNum { get; set; }

        [Required]
        public bool IsSubjectToEInvoice { get; set; }

        [Required]
        public bool IsSubjectToEShipment { get; set; }

        [Required]
        public bool IsArrangeCommercialInvoice { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime AgreementDate { get; set; }

        [Required]
        public short PaymentTerm { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string DueDateFormulaCode { get; set; }

        [Required]
        public bool UseManufacturing { get; set; }

        [Required]
        public bool PermitCreditBalance { get; set; }

        [Required]
        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string DataLanguageCode { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string CurrencyCode { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal CreditLimit { get; set; }

        [Required]
        public byte ExchangeTypeCode { get; set; }

        [Required]
        public bool AllowOnlySelectedCurrency { get; set; }

        [Required]
        public bool IsVIP { get; set; }

        [Required]
        public bool IsSendAdvertSMS { get; set; }

        [Required]
        public bool IsSendAdvertMail { get; set; }

        [Required]
        public byte CustomerTypeCode { get; set; }

        [Required]
        public byte VendorTypeCode { get; set; }

        [Required]
        public int StoreHierarchyID { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string CustomerDiscountGrCode { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string CustomerMarkupGrCode { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string CurrAccLotGrCode { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string CustomerPaymentPlanGrCode { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string RetailSalePriceGroupCode { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string WholesalePriceGroupCode { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PromotionGroupCode { get; set; }

        [Required]
        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string SalesChannelCode { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BarcodeTypeCode { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CostCenterCode { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string GLTypeCode { get; set; }

        [Required]
        public bool CustomerASNNumberIsRequiredForShipments { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string BankCode { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankBranchCode { get; set; }

        [Required]
        public byte BankAccTypeCode { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IBAN { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SWIFTCode { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankAccNo { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal MinBalance { get; set; }

        [Required]
        public bool UseBankAccOnStore { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime AccountOpeningDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime AccountClosingDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime EInvoiceStartDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime EShipmentStartDate { get; set; }

        public Guid? EInvoiceConfirmationRuleID { get; set; }

        [Required]
        public bool PurchaseRequisitionRequired { get; set; }

        [Required]
        public bool UseDBSIntegration { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string DBSAccountCode { get; set; }

        [Required]
        public bool UseSerialNumberTracking { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime LockedDate { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string VendorPaymentPlanGrCode { get; set; }

        [InverseProperty("cdCurrAcc")]
        public virtual ICollection<cdCurrAccDesc> cdCurrAccDescs { get; set; }

        [InverseProperty("cdCurrAcc")]
        public virtual ICollection<prCustomerVendorAccount> prCustomerVendorAccounts { get; set; }

        [InverseProperty("cdCurrAcc")]
        public virtual ICollection<prCurrAccDefault> prCurrAccDefaults { get; set; }

        [InverseProperty("cdCurrAcc")]
        public virtual ICollection<prCurrAccPostalAddress> prCurrAccPostalAddresss { get; set; }

        [ForeignKey("BankAccTypeCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdBankAccType cdBankAccType { get; set; }

        [ForeignKey("BankCode,BankBranchCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual prBankBranch prBankBranch { get; set; }

        [ForeignKey("BarcodeTypeCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdBarcodeType cdBarcodeType { get; set; }

        [ForeignKey("CompanyCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdCompany cdCompany { get; set; }

        [ForeignKey("CostCenterCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdCostCenter cdCostCenter { get; set; }

        [ForeignKey("CurrAccLotGrCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdCurrAccLotGr cdCurrAccLotGr { get; set; }

        [ForeignKey("CurrAccTypeCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual bsCurrAccType bsCurrAccType { get; set; }

        [ForeignKey("CurrencyCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdCurrency cdCurrency { get; set; }

        [ForeignKey("CustomerDiscountGrCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdCustomerDiscountGr cdCustomerDiscountGr { get; set; }

        [ForeignKey("CustomerMarkupGrCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdCustomerMarkupGr cdCustomerMarkupGr { get; set; }

        [ForeignKey("CustomerPaymentPlanGrCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdCustomerPaymentPlanGr cdCustomerPaymentPlanGr { get; set; }

        [ForeignKey("CustomerTypeCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual bsCustomerType bsCustomerType { get; set; }

        [ForeignKey("DataLanguageCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdDataLanguage cdDataLanguage { get; set; }

        [ForeignKey("DueDateFormulaCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdDueDateFormula cdDueDateFormula { get; set; }

        [ForeignKey("EInvoiceConfirmationRuleID")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdConfirmationRule cdConfirmationRule { get; set; }

        [ForeignKey("ExchangeTypeCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdExchangeType cdExchangeType { get; set; }

        [ForeignKey("GLTypeCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdGLType cdGLType { get; set; }

        [ForeignKey("OfficeCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdOffice cdOffice { get; set; }

        [ForeignKey("PromotionGroupCode")]
        public virtual cdPromotionGroup cdPromotionGroup { get; set; }

        [ForeignKey("RetailSalePriceGroupCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdPriceGroup cdPriceGroup { get; set; }

        [ForeignKey("SalesChannelCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdSalesChannel cdSalesChannel { get; set; }

        [ForeignKey("StoreHierarchyID")]
        [InverseProperty("cdCurrAccs")]
        public virtual dfStoreHierarchy dfStoreHierarchy { get; set; }

        [ForeignKey("TaxOfficeCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdTaxOffice cdTaxOffice { get; set; }

        [ForeignKey("TitleCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdTitle cdTitle { get; set; }

        [ForeignKey("VendorPaymentPlanGrCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdVendorPaymentPlanGr cdVendorPaymentPlanGr { get; set; }

        [ForeignKey("VendorTypeCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual bsVendorType bsVendorType { get; set; }

        [ForeignKey("WholesalePriceGroupCode")]
        [InverseProperty("cdCurrAccs")]
        public virtual cdPriceGroup cdPriceGroup1 { get; set; }
    }
}
