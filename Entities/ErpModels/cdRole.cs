using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdRole")]
    public partial class cdRole
    {
        public cdRole()
        {
            auBankPermits = new HashSet<auBankPermit>();
            auBasePricePermits = new HashSet<auBasePricePermit>();
            auCardColumnPermits = new HashSet<auCardColumnPermit>();
            auCardElementPermits = new HashSet<auCardElementPermit>();
            auCardElementRequiredKeys = new HashSet<auCardElementRequiredKey>();
            auCardPermits = new HashSet<auCardPermit>();
            auCashPermits = new HashSet<auCashPermit>();
            auChequePermits = new HashSet<auChequePermit>();
            auCreditCardPaymentPermits = new HashSet<auCreditCardPaymentPermit>();
            auCustomTablePermits = new HashSet<auCustomTablePermit>();
            auDebitPermits = new HashSet<auDebitPermit>();
            auExpenseSlipPermits = new HashSet<auExpenseSlipPermit>();
            auInnerProcessPermits = new HashSet<auInnerProcessPermit>();
            auItemTests = new HashSet<auItemTest>();
            auJournalPermits = new HashSet<auJournalPermit>();
            auMobileStorePermits = new HashSet<auMobileStorePermit>();
            auPaymentPermits = new HashSet<auPaymentPermit>();
            auPriceListPermits = new HashSet<auPriceListPermit>();
            auProcessPermits = new HashSet<auProcessPermit>();
            auProformaProcessPermits = new HashSet<auProformaProcessPermit>();
            auProgramPermits = new HashSet<auProgramPermit>();
            auPurchaseRequisitionPermits = new HashSet<auPurchaseRequisitionPermit>();
            auPurchaseRequisitionProposalPermits = new HashSet<auPurchaseRequisitionProposalPermit>();
            auReportFilterMinMaxDateValues = new HashSet<auReportFilterMinMaxDateValue>();
            auReportQueryPermits = new HashSet<auReportQueryPermit>();
            auSupportRequests = new HashSet<auSupportRequest>();
            auSurveyPermits = new HashSet<auSurveyPermit>();
            auSurveySectionPermits = new HashSet<auSurveySectionPermit>();
            auVehicleLoadingPermits = new HashSet<auVehicleLoadingPermit>();
            auVehicleUnLoadingPermits = new HashSet<auVehicleUnLoadingPermit>();
            cdRoleDescs = new HashSet<cdRoleDesc>();
            prRoleMembers = new HashSet<prRoleMember>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RoleCode { get; set; }

        [Required]
        public byte UserTypeCode { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<auBankPermit> auBankPermits { get; set; }
        public virtual ICollection<auBasePricePermit> auBasePricePermits { get; set; }
        public virtual ICollection<auCardColumnPermit> auCardColumnPermits { get; set; }
        public virtual ICollection<auCardElementPermit> auCardElementPermits { get; set; }
        public virtual ICollection<auCardElementRequiredKey> auCardElementRequiredKeys { get; set; }
        public virtual ICollection<auCardPermit> auCardPermits { get; set; }
        public virtual ICollection<auCashPermit> auCashPermits { get; set; }
        public virtual ICollection<auChequePermit> auChequePermits { get; set; }
        public virtual ICollection<auCreditCardPaymentPermit> auCreditCardPaymentPermits { get; set; }
        public virtual ICollection<auCustomTablePermit> auCustomTablePermits { get; set; }
        public virtual ICollection<auDebitPermit> auDebitPermits { get; set; }
        public virtual ICollection<auExpenseSlipPermit> auExpenseSlipPermits { get; set; }
        public virtual ICollection<auInnerProcessPermit> auInnerProcessPermits { get; set; }
        public virtual ICollection<auItemTest> auItemTests { get; set; }
        public virtual ICollection<auJournalPermit> auJournalPermits { get; set; }
        public virtual ICollection<auMobileStorePermit> auMobileStorePermits { get; set; }
        public virtual ICollection<auPaymentPermit> auPaymentPermits { get; set; }
        public virtual ICollection<auPriceListPermit> auPriceListPermits { get; set; }
        public virtual ICollection<auProcessPermit> auProcessPermits { get; set; }
        public virtual ICollection<auProformaProcessPermit> auProformaProcessPermits { get; set; }
        public virtual ICollection<auProgramPermit> auProgramPermits { get; set; }
        public virtual ICollection<auPurchaseRequisitionPermit> auPurchaseRequisitionPermits { get; set; }
        public virtual ICollection<auPurchaseRequisitionProposalPermit> auPurchaseRequisitionProposalPermits { get; set; }
        public virtual ICollection<auReportFilterMinMaxDateValue> auReportFilterMinMaxDateValues { get; set; }
        public virtual ICollection<auReportQueryPermit> auReportQueryPermits { get; set; }
        public virtual ICollection<auSupportRequest> auSupportRequests { get; set; }
        public virtual ICollection<auSurveyPermit> auSurveyPermits { get; set; }
        public virtual ICollection<auSurveySectionPermit> auSurveySectionPermits { get; set; }
        public virtual ICollection<auVehicleLoadingPermit> auVehicleLoadingPermits { get; set; }
        public virtual ICollection<auVehicleUnLoadingPermit> auVehicleUnLoadingPermits { get; set; }
        public virtual ICollection<cdRoleDesc> cdRoleDescs { get; set; }
        public virtual ICollection<prRoleMember> prRoleMembers { get; set; }
    }
}
