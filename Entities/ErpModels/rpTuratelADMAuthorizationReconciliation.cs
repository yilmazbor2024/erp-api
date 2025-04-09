using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpTuratelADMAuthorizationReconciliation")]
    public partial class rpTuratelADMAuthorizationReconciliation
    {
        public rpTuratelADMAuthorizationReconciliation()
        {
        }

        [Key]
        [Required]
        public Guid TuratelADMAuthorizationReconciliationID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Msisdn { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string EMailAddress { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string KvkkProcessValue { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string KvkkProcessAuthorizationDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string KvkkShareValue { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string KvkkShareAuthorizationDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string KvkkInternationalTransferValue { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string KvkkInternationalTransferAuthorizationDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string EtkMsisdnSmsValue { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EtkMsisdnSmsAuthorizationDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string EtkMsisdnSmsIysRecordSource { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string EtkMsisdnCallValue { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EtkMsisdnCallAuthorizationDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string EtkMsisdnCallIysRecordSource { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string EtkEMailAddressEMailValue { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EtkEMailAddressEMailAuthorizationDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string EtkEMailAddressEMailIysRecordSource { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string EtkMsisdnSmsBusinessValue { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EtkMsisdnSmsBusinessAuthorizationDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string EtkMsisdnSmsBusinessIysRecordSource { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string EtkMsisdnCallBusinessValue { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EtkMsisdnCallBusinessAuthorizationDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string EtkMsisdnCallBusinessIysRecordSource { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string EtkEMailAddressEMailBusinessValue { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EtkEMailAddressEMailBusinessAuthorizationDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string EtkEMailAddressEMailBusinessIysRecordSource { get; set; }

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
