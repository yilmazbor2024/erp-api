using System;
using System.Collections.Generic;
using ErpMobile.Api.Models.Contact;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Request model for creating a new customer.
    /// </summary>
    public class CustomerCreateRequest
    {
        /// <summary>
        /// Geçici müşteri kayıt token'ı (opsiyonel)
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the customer code.
        /// </summary>
        [StringLength(50)]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        [Required]
        [StringLength(250)]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the customer surname.
        /// </summary>
        [StringLength(100)]
        public string CustomerSurname { get; set; }

        /// <summary>
        /// Gets or sets the customer title code.
        /// </summary>
        [StringLength(10)]
        public string TitleCode { get; set; }

        /// <summary>
        /// Gets or sets the patronym (middle name).
        /// </summary>
        [StringLength(60)]
        public string Patronym { get; set; }

        /// <summary>
        /// Gets or sets the tax number.
        /// </summary>
        [StringLength(20)]
        public string TaxNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer identity number.
        /// </summary>
        [StringLength(20)]
        public string CustomerIdentityNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the mersis number.
        /// </summary>
        [StringLength(20)]
        public string MersisNum { get; set; }

        /// <summary>
        /// Gets or sets the customer type code.
        /// </summary>
        public int CustomerTypeCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is an individual account.
        /// </summary>
        public bool IsIndividualAcc { get; set; }

        /// <summary>
        /// Gets or sets the discount group code.
        /// </summary>
        [StringLength(10)]
        public string DiscountGroupCode { get; set; }

        /// <summary>
        /// Gets or sets the customer markup group code.
        /// </summary>
        [StringLength(10)]
        public string CustomerMarkupGrCode { get; set; }

        /// <summary>
        /// Gets or sets the customer payment plan group code.
        /// </summary>
        [StringLength(10)]
        public string CustomerPaymentPlanGrCode { get; set; }

        /// <summary>
        /// Gets or sets the vendor payment plan group code.
        /// </summary>
        [StringLength(10)]
        public string VendorPaymentPlanGrCode { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        [StringLength(10)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the office code.
        /// </summary>
        [StringLength(10)]
        public string OfficeCode { get; set; } = "M";

        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        [StringLength(10)]
        public string CompanyCode { get; set; } = "1";

        /// <summary>
        /// Gets or sets the salesman code.
        /// </summary>
        [StringLength(20)]
        public string SalesmanCode { get; set; }

        /// <summary>
        /// Gets or sets the credit limit.
        /// </summary>
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// Gets or sets the risk limit.
        /// </summary>
        public decimal RiskLimit { get; set; }

        /// <summary>
        /// Gets or sets the minimum balance.
        /// </summary>
        public decimal MinBalance { get; set; }

        /// <summary>
        /// Gets or sets the list of customer contacts.
        /// </summary>
        public List<ContactCreateRequest> Contacts { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of customer addresses.
        /// </summary>
        public List<CustomerAddressCreateRequest> Addresses { get; set; } = new();

        /// <summary>
        /// Gets or sets the tax office code.
        /// </summary>
        [StringLength(10)]
        public string TaxOfficeCode { get; set; }

        /// <summary>
        /// Gets or sets the region code.
        /// </summary>
        [StringLength(10)]
        public string RegionCode { get; set; }

        /// <summary>
        /// Gets or sets the city code.
        /// </summary>
        [StringLength(10)]
        public string CityCode { get; set; }

        /// <summary>
        /// Gets or sets the district code.
        /// </summary>
        [StringLength(10)]
        public string DistrictCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is blocked.
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is locked.
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Gets or sets the locked date.
        /// </summary>
        public DateTime? LockedDate { get; set; }

        /// <summary>
        /// Gets or sets the list of customer communications.
        /// </summary>
        public List<CustomerCommunicationCreateRequest> Communications { get; set; } = new();

        /// <summary>
        /// Gets or sets a value indicating whether the customer is VIP.
        /// </summary>
        public bool IsVIP { get; set; }
        
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        [StringLength(10)]
        public string CountryCode { get; set; }
        
        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        [StringLength(10)]
        public string StateCode { get; set; }
        
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [StringLength(500)]
        public string Address { get; set; }
        
        /// <summary>
        /// Gets or sets the tax office.
        /// </summary>
        [StringLength(100)]
        public string TaxOffice { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the customer is an individual.
        /// </summary>
        public bool IsIndividual { get; set; }
        
        /// <summary>
        /// Gets or sets the promotion group code.
        /// </summary>
        [StringLength(10)]
        public string PromotionGroupCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is a real person.
        /// </summary>
        public bool IsRealPerson { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to send advertisement SMS.
        /// </summary>
        public bool IsSendAdvertSMS { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to send advertisement mail.
        /// </summary>
        public bool IsSendAdvertMail { get; set; }
        
        /// <summary>
        /// Gets or sets the created user name.
        /// </summary>
        [StringLength(50)]
        public string CreatedUserName { get; set; } = "SYSTEM";
        
        /// <summary>
        /// Gets or sets the last updated user name.
        /// </summary>
        [StringLength(50)]
        public string LastUpdatedUserName { get; set; } = "SYSTEM";

 

        /// <summary>
        /// Gets or sets the due date formula code.
        /// </summary>
        [StringLength(10)]
        public string DueDateFormulaCode { get; set; }

        /// <summary>
        /// Gets or sets the bank code.
        /// </summary>
        [StringLength(10)]
        public string BankCode { get; set; }

        /// <summary>
        /// Gets or sets the bank branch code.
        /// </summary>
        [StringLength(20)]
        public string BankBranchCode { get; set; }

        /// <summary>
        /// Gets or sets the bank account type code.
        /// </summary>
        public int BankAccTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the IBAN.
        /// </summary>
        [StringLength(30)]
        public string IBAN { get; set; }

        /// <summary>
        /// Gets or sets the SWIFT code.
        /// </summary>
        [StringLength(20)]
        public string SWIFTCode { get; set; }

        /// <summary>
        /// Gets or sets the bank account number.
        /// </summary>
        [StringLength(20)]
        public string BankAccNo { get; set; }

        /// <summary>
        /// Gets or sets the vendor type code.
        /// </summary>
        public int VendorTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the retail sale price group code.
        /// </summary>
        [StringLength(10)]
        public string RetailSalePriceGroupCode { get; set; }

        /// <summary>
        /// Gets or sets the wholesale price group code.
        /// </summary>
        [StringLength(10)]
        public string WholesalePriceGroupCode { get; set; }

        /// <summary>
        /// Gets or sets the account opening date.
        /// </summary>
        public DateTime? AccountOpeningDate { get; set; }

        /// <summary>
        /// Gets or sets the account closing date.
        /// </summary>
        public DateTime? AccountClosingDate { get; set; }

        /// <summary>
        /// Gets or sets the sales channel code.
        /// </summary>
        [StringLength(5)]
        public string SalesChannelCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use manufacturing.
        /// </summary>
        public bool UseManufacturing { get; set; }

        /// <summary>
        /// Gets or sets the barcode type code.
        /// </summary>
        [StringLength(20)]
        public string BarcodeTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the cost center code.
        /// </summary>
        [StringLength(20)]
        public string CostCenterCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use bank account on store.
        /// </summary>
        public bool UseBankAccOnStore { get; set; }

        /// <summary>
        /// Gets or sets the GL type code.
        /// </summary>
        [StringLength(20)]
        public string GLTypeCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is subject to e-invoice.
        /// </summary>
        public bool IsSubjectToEInvoice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to arrange commercial invoice.
        /// </summary>
        public bool IsArrangeCommercialInvoice { get; set; }

        /// <summary>
        /// Gets or sets the CurrAccLotGrCode.
        /// </summary>
        [StringLength(10)]
        public string CurrAccLotGrCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to allow only selected currency.
        /// </summary>
        public bool AllowOnlySelectedCurrency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to permit credit balance.
        /// </summary>
        public bool PermitCreditBalance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is subject to e-shipment.
        /// </summary>
        public bool IsSubjectToEShipment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customer ASN number is required for shipments.
        /// </summary>
        public bool CustomerASNNumberIsRequiredForShipments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether purchase requisition is required.
        /// </summary>
        public bool PurchaseRequisitionRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use DBS integration.
        /// </summary>
        public bool UseDBSIntegration { get; set; }

        /// <summary>
        /// Gets or sets the DBS account code.
        /// </summary>
        [StringLength(30)]
        public string DBSAccountCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use serial number tracking.
        /// </summary>
        public bool UseSerialNumberTracking { get; set; }

        /// <summary>
        /// Gets or sets the e-invoice start date.
        /// </summary>
        public DateTime? EInvoiceStartDate { get; set; }

        /// <summary>
        /// Gets or sets the e-shipment start date.
        /// </summary>
        public DateTime? EShipmentStartDate { get; set; }

        /// <summary>
        /// Gets or sets the data language code.
        /// </summary>
        [StringLength(5)]
        public string DataLanguageCode { get; set; } = "TR";

        /// <summary>
        /// Gets or sets the store hierarchy ID.
        /// </summary>
        public int StoreHierarchyID { get; set; }

        /// <summary>
        /// Gets or sets the agreement date.
        /// </summary>
        public DateTime? AgreementDate { get; set; }

        /// <summary>
        /// Gets or sets the payment term.
        /// </summary>
        public short PaymentTerm { get; set; }
    }
}