using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpTuratelACLWhiteListReconciliation")]
    public partial class rpTuratelACLWhiteListReconciliation
    {
        public rpTuratelACLWhiteListReconciliation()
        {
        }

        [Key]
        [Required]
        public Guid TuratelACLWhiteListReconciliationID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Subscriber { get; set; }

        [Required]
        public int SubscriberType { get; set; }

        [Required]
        public int CommunicationType { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SourceProcessId { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string IysSource { get; set; }

        [Required]
        public int RelationType { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

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
