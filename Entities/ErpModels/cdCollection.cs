using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCollection")]
    public partial class cdCollection
    {
        public cdCollection()
        {
            cdCollectionDescs = new HashSet<cdCollectionDesc>();
            cdProductCollectionGrs = new HashSet<cdProductCollectionGr>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CollectionCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ProductCodeParameter { get; set; }

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

        public virtual ICollection<cdCollectionDesc> cdCollectionDescs { get; set; }
        public virtual ICollection<cdProductCollectionGr> cdProductCollectionGrs { get; set; }
    }
}
