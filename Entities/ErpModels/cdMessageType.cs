using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdMessageType")]
    public partial class cdMessageType
    {
        public cdMessageType()
        {
            cdMessageTypeDescs = new HashSet<cdMessageTypeDesc>();
            trMessageBoxs = new HashSet<trMessageBox>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MessageTypeCode { get; set; }

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

        public virtual ICollection<cdMessageTypeDesc> cdMessageTypeDescs { get; set; }
        public virtual ICollection<trMessageBox> trMessageBoxs { get; set; }
    }
}
