using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfSMSForCustomerRelationship")]
    public partial class dfSMSForCustomerRelationship
    {
        public dfSMSForCustomerRelationship()
        {
            lgSMSForCustomerRelationshipNonFormattedCommunicationss = new HashSet<lgSMSForCustomerRelationshipNonFormattedCommunications>();
        }

        [Key]
        [Required]
        public Guid SMSForCustomerRelationshipID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SMSJobName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SMSJobTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Originator { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ReportName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string QueryName { get; set; }

        [Required]
        public byte SourceApplication { get; set; }

        public string FilterString { get; set; }

        public string FilterStringForSql { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrAccListCode { get; set; }

        [Required]
        public bool SourceFromFile { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object FileName { get; set; }

        [Required]
        public byte DiscountOfferStageCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string DiscountOfferCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object MessageTemplate { get; set; }

        [Required]
        public int ScheduleID { get; set; }

        [Required]
        public short RetryCountForUndeliveredSMS { get; set; }

        [Required]
        public short ExpireSMSAfterNHours { get; set; }

        [Required]
        public short RetryCountForNoAction { get; set; }

        [Required]
        public short RetryPeriodForNoAction { get; set; }

        [Required]
        public int PackageContentCount { get; set; }

        [Required]
        public short WaitTimeForNextPackage { get; set; }

        [Required]
        public bool CheckOptinOutStatus { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SMSGatewayServiceCode { get; set; }

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
        public virtual cdCurrAccList cdCurrAccList { get; set; }
        public virtual cdCompanyBrand cdCompanyBrand { get; set; }
        public virtual cdScheduleSMSForCustomerRelationship cdScheduleSMSForCustomerRelationship { get; set; }
        public virtual cdSMSJobType cdSMSJobType { get; set; }
        public virtual bsCurrAccType bsCurrAccType { get; set; }

        public virtual ICollection<lgSMSForCustomerRelationshipNonFormattedCommunications> lgSMSForCustomerRelationshipNonFormattedCommunicationss { get; set; }
    }
}
