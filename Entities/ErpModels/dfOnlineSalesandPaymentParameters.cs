using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfOnlineSalesandPaymentParameters")]
    public partial class dfOnlineSalesandPaymentParameters
    {
        public dfOnlineSalesandPaymentParameters()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DistanceSalesContractWebAdress { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DistanceSalesContractReportFile { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string OrderSummaryReportFile { get; set; }

        [Required]
        public short LinkExpireTime { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SalesURL { get; set; }

        [Required]
        public byte PaymentTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentTypeDescription { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentAgent { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryCompanyCode { get; set; }

        [Required]
        public short AutoOrderCancelTime { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string AutoCancelReasonCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string BankPaymentCodePrefix { get; set; }

        [Required]
        public bool SendSMSForBankPaymentCode { get; set; }

        [Required]
        public bool SendSMSForBankPaymentRealised { get; set; }

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
        public virtual bsInternetPaymentType bsInternetPaymentType { get; set; }
        public virtual cdDeliveryCompany cdDeliveryCompany { get; set; }
        public virtual cdCompanyBrand cdCompanyBrand { get; set; }
        public virtual cdOrderCancelReason cdOrderCancelReason { get; set; }

    }
}
