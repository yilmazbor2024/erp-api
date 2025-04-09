using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trSMSPoolLine")]
    public partial class trSMSPoolLine
    {
        public trSMSPoolLine()
        {
            tpSMSPoolLineExtensions = new HashSet<tpSMSPoolLineExtension>();
        }

        [Key]
        [Required]
        public Guid SMSPoolLineID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMail { get; set; }

        [Required]
        public Guid SMSPoolMessageID { get; set; }

        [Required]
        public bool IsRetried { get; set; }

        [Required]
        public byte SMSStatusCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MessageResponseID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string MessageErrorReason { get; set; }

        [Required]
        public byte DeliveryStatus { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public Guid SMSPoolHeaderID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual bsSMSStatus bsSMSStatus { get; set; }
        public virtual trSMSPoolMessage trSMSPoolMessage { get; set; }
        public virtual trSMSPoolHeader trSMSPoolHeader { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<tpSMSPoolLineExtension> tpSMSPoolLineExtensions { get; set; }
    }
}
