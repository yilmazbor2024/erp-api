using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdProductDimSet")]
    public partial class cdProductDimSet
    {
        public cdProductDimSet()
        {
            cdProductDimSetDescs = new HashSet<cdProductDimSetDesc>();
            dfProductHierarchyDimSets = new HashSet<dfProductHierarchyDimSet>();
            prProductDimSetContents = new HashSet<prProductDimSetContent>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ProductDimSetCode { get; set; }

        [Required]
        public byte ItemDimTypeCode { get; set; }

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

        // Navigation Properties
        public virtual bsItemDimType bsItemDimType { get; set; }

        public virtual ICollection<cdProductDimSetDesc> cdProductDimSetDescs { get; set; }
        public virtual ICollection<dfProductHierarchyDimSet> dfProductHierarchyDimSets { get; set; }
        public virtual ICollection<prProductDimSetContent> prProductDimSetContents { get; set; }
    }
}
