using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trSMSPoolHeader")]
    public partial class trSMSPoolHeader
    {
        public trSMSPoolHeader()
        {
            trSMSPoolLines = new HashSet<trSMSPoolLine>();
            trSMSPoolMessages = new HashSet<trSMSPoolMessage>();
        }

        [Key]
        [Required]
        public Guid SMSPoolHeaderID { get; set; }

        [Required]
        public bool IsSent { get; set; }

        [Required]
        public DateTime SendDate { get; set; }

        [Required]
        public bool IsBusy { get; set; }

        [Required]
        public bool IsCommonMessageText { get; set; }

        [Required]
        public int MessageReasonCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Originator { get; set; }

        [Required]
        public bool IsMail { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MessageResponseID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string MessageSubject { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SMSGatewayServiceCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SMSGatewayMessageType { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SMSGatewayRecipientType { get; set; }

        public Guid? SMSForCustomerRelationshipID { get; set; }

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
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdMessageReason cdMessageReason { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<trSMSPoolLine> trSMSPoolLines { get; set; }
        public virtual ICollection<trSMSPoolMessage> trSMSPoolMessages { get; set; }
    }
}
