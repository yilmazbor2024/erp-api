using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpSupportRequestConfirmation")]
    public partial class tpSupportRequestConfirmation
    {
        public tpSupportRequestConfirmation()
        {
        }

        [Key]
        [Required]
        public Guid SupportRequestConfirmationID { get; set; }

        [Required]
        public Guid SupportRequestHeaderID { get; set; }

        [Required]
        public Guid SupportResolveID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationReasonCode { get; set; }

        [Required]
        public DateTime ConfirmationDate { get; set; }

        [Required]
        public DateTime RejectDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

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
        public virtual trSupportRequestHeader trSupportRequestHeader { get; set; }
        public virtual cdConfirmationReason cdConfirmationReason { get; set; }

    }
}
