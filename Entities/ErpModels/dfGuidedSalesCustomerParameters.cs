using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfGuidedSalesCustomerParameters")]
    public partial class dfGuidedSalesCustomerParameters
    {
        public dfGuidedSalesCustomerParameters()
        {
        }

        [Key]
        [Required]
        public Guid GuidedSalesCustomerParametersID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string VisibleCurrAccAttTypes { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string VisibleCustomerCompanyBrandAttTypes { get; set; }

        public byte? CurrAccAttTypeForMonetaryValue { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CurrAccAttCodeForMonetaryLowValue { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CurrAccAttCodeForMonetaryHighValue { get; set; }

        public byte? CustomerCompanyBrandAttTypeForMonetaryValue { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerCompanyBrandAttCodeForMonetaryLowValue { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerCompanyBrandAttCodeForMonetaryHighValue { get; set; }

        public byte? CurrAccAttTypeForBehavioralValue { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CurrAccAttCodeForBehavioralLowValue { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CurrAccAttCodeForBehavioralHighValue { get; set; }

        public byte? CustomerCompanyBrandAttTypeForBehavioralValue { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerCompanyBrandAttCodeForBehavioralLowValue { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerCompanyBrandAttCodeForBehavioralHighValue { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EmailCommunicationTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string MobileCommunicationTypeCode { get; set; }

        public bool? CustomerInformationCannotBeChanged { get; set; }

        public bool? ShowCustomerAlertColor { get; set; }

        public byte? BeforeBirthDate { get; set; }

        public byte? AfterBirthDate { get; set; }

        public byte? BeforeMarriedDate { get; set; }

        public byte? AfterMarriedDate { get; set; }

        public bool? UseSMSVerificationOnMS { get; set; }

        public bool? UseSMSVerificationOnMSForValidDiscounts { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DefRetailCustomerCodeMobileStore { get; set; }

        [Required]
        public bool UseAlwaysDefRetailCustomerMobileStore { get; set; }

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

    }
}
