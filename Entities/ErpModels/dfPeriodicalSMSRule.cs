using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfPeriodicalSMSRule")]
    public partial class dfPeriodicalSMSRule
    {
        public dfPeriodicalSMSRule()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Originator { get; set; }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [Required]
        public int MessageReasonCode { get; set; }

        [Key]
        [Required]
        public bool IsMail { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string MessageSubject { get; set; }

        [Required]
        public TimeSpan SendTime { get; set; }

        [Required]
        public short RepetitionPeriod { get; set; }

        [Required]
        public byte RemaindNDaysBefore { get; set; }

        [Required]
        public decimal MinAmount { get; set; }

        [Required]
        public decimal MaxAmount { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object MessageText { get; set; }

        [Required]
        public bool IsEnabled { get; set; }

        [Required]
        public bool IsForInstantSms { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdMessageReason cdMessageReason { get; set; }

    }
}
