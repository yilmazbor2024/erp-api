using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPaymentPlan")]
    public partial class cdPaymentPlan
    {
        public cdPaymentPlan()
        {
            cdPaymentPlanDescs = new HashSet<cdPaymentPlanDesc>();
            dfPaynetPaymentPlans = new HashSet<dfPaynetPaymentPlan>();
            prCustomerPaymentPlanGrAtts = new HashSet<prCustomerPaymentPlanGrAtt>();
            prItemPaymentPlanGrAtts = new HashSet<prItemPaymentPlanGrAtt>();
            prPaymentPlanAdditionalInstallmentAuthoritys = new HashSet<prPaymentPlanAdditionalInstallmentAuthority>();
            prPaymentPlanAdditionalInstallmentCampaigns = new HashSet<prPaymentPlanAdditionalInstallmentCampaign>();
            prPaymentPlanBINs = new HashSet<prPaymentPlanBIN>();
            prProcessInfos = new HashSet<prProcessInfo>();
            prVendorPaymentPlanGrAtts = new HashSet<prVendorPaymentPlanGrAtt>();
            trContracts = new HashSet<trContract>();
            trDepartmentReceiptLines = new HashSet<trDepartmentReceiptLine>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trOrderLines = new HashSet<trOrderLine>();
            trPriceListLines = new HashSet<trPriceListLine>();
            trProposalLines = new HashSet<trProposalLine>();
            trShipmentLines = new HashSet<trShipmentLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentPlanCode { get; set; }

        [Required]
        public float PaymentPercentage { get; set; }

        [Required]
        public byte InstallmentCount { get; set; }

        [Required]
        public bool ForCreditCardPlan { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DueDateFormulaCode { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

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

        [Required]
        public bool UseDeviceIntegrationInstallments { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string AvailableInstallments { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LikeCashInstallments { get; set; }

        // Navigation Properties
        public virtual cdDueDateFormula cdDueDateFormula { get; set; }

        public virtual ICollection<cdPaymentPlanDesc> cdPaymentPlanDescs { get; set; }
        public virtual ICollection<dfPaynetPaymentPlan> dfPaynetPaymentPlans { get; set; }
        public virtual ICollection<prCustomerPaymentPlanGrAtt> prCustomerPaymentPlanGrAtts { get; set; }
        public virtual ICollection<prItemPaymentPlanGrAtt> prItemPaymentPlanGrAtts { get; set; }
        public virtual ICollection<prPaymentPlanAdditionalInstallmentAuthority> prPaymentPlanAdditionalInstallmentAuthoritys { get; set; }
        public virtual ICollection<prPaymentPlanAdditionalInstallmentCampaign> prPaymentPlanAdditionalInstallmentCampaigns { get; set; }
        public virtual ICollection<prPaymentPlanBIN> prPaymentPlanBINs { get; set; }
        public virtual ICollection<prProcessInfo> prProcessInfos { get; set; }
        public virtual ICollection<prVendorPaymentPlanGrAtt> prVendorPaymentPlanGrAtts { get; set; }
        public virtual ICollection<trContract> trContracts { get; set; }
        public virtual ICollection<trDepartmentReceiptLine> trDepartmentReceiptLines { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trPriceListLine> trPriceListLines { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
    }
}
