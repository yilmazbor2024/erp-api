using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdFrameType")]
    public partial class cdFrameType
    {
        public cdFrameType()
        {
            cdFrameTypeDescs = new HashSet<cdFrameTypeDesc>();
            prProductFramePropertiess = new HashSet<prProductFrameProperties>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string FrameTypeCode { get; set; }

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

        public virtual ICollection<cdFrameTypeDesc> cdFrameTypeDescs { get; set; }
        public virtual ICollection<prProductFrameProperties> prProductFramePropertiess { get; set; }
    }
}
