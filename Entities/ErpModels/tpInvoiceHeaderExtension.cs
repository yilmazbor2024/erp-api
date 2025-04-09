using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceHeaderExtension")]
    public partial class tpInvoiceHeaderExtension
    {
        public tpInvoiceHeaderExtension()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Required]
        public byte InvoiceReturnTypeCode { get; set; }

        [Required]
        public bool IsIndividual { get; set; }

        [Required]
        public bool EnteredFromEDocumentPortal { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PayeeFinancialAccountID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PaymentMeansCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PaymentChannelCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GTBRefNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GCBRegNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxFreeRefundCompanyCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string InsuranceAgencyCode { get; set; }

        [Required]
        public byte MedulaDeclarationStatus { get; set; }

        [Required]
        public bool ReturnNotified { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ExportTypeCode { get; set; }

        [Required]
        public DateTime ATAttributeUpdatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAttributeUpdatedUserName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LateChargeRateChangeDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OnlineDBSWebServiceCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OnlineDBSID { get; set; }

        [Required]
        public int DBSStatusCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DBSBankCode { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public byte DiscountSubReasonCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DiscountReasonDescription { get; set; }

        public Guid? AgentReservationHeaderID { get; set; }

        [Required]
        public byte ItemReturnDeliveryType { get; set; }

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
        public bool IsWebStoreInvoice { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramLevelCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramStatusCode { get; set; }

        // Navigation Properties
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual cdInsuranceAgency cdInsuranceAgency { get; set; }
        public virtual cdLoyaltyProgramStatus cdLoyaltyProgramStatus { get; set; }
        public virtual bsPaymentMeans bsPaymentMeans { get; set; }
        public virtual cdDiscountSubReason cdDiscountSubReason { get; set; }
        public virtual cdLoyaltyProgramLevel cdLoyaltyProgramLevel { get; set; }
        public virtual trAgentReservationHeader trAgentReservationHeader { get; set; }
        public virtual cdBank cdBank { get; set; }
        public virtual cdExportType cdExportType { get; set; }
        public virtual cdLoyaltyProgram cdLoyaltyProgram { get; set; }
        public virtual bsInvoiceReturnType bsInvoiceReturnType { get; set; }
        public virtual bsTaxFreeRefundCompany bsTaxFreeRefundCompany { get; set; }

    }
}
