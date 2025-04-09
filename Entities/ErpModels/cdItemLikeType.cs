using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdItemLikeType")]
    public partial class cdItemLikeType
    {
        public cdItemLikeType()
        {
            cdItemLikeTypeDescs = new HashSet<cdItemLikeTypeDesc>();
            prItemAlikes = new HashSet<prItemAlike>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ItemLikeTypeCode { get; set; }

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

        public virtual ICollection<cdItemLikeTypeDesc> cdItemLikeTypeDescs { get; set; }
        public virtual ICollection<prItemAlike> prItemAlikes { get; set; }
    }
}
