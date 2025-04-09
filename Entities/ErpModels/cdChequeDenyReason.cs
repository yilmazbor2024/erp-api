using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdChequeDenyReason")]
    public partial class cdChequeDenyReason
    {
        public cdChequeDenyReason()
        {
            auChequeDenys = new HashSet<auChequeDeny>();
            cdChequeDenyReasonDescs = new HashSet<cdChequeDenyReasonDesc>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ChequeDenyReasonCode { get; set; }

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

        public virtual ICollection<auChequeDeny> auChequeDenys { get; set; }
        public virtual ICollection<cdChequeDenyReasonDesc> cdChequeDenyReasonDescs { get; set; }
    }
}
