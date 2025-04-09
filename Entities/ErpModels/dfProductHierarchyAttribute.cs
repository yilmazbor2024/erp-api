using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfProductHierarchyAttribute")]
    public partial class dfProductHierarchyAttribute
    {
        public dfProductHierarchyAttribute()
        {
        }

        [Key]
        [Required]
        public int ProductHierarchyID { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttributeCode { get; set; }

        [Required]
        public bool IsUnchangeable { get; set; }

        [Required]
        public bool IsRequired { get; set; }

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
        public virtual cdItemAttribute cdItemAttribute { get; set; }
        public virtual dfProductHierarchy dfProductHierarchy { get; set; }

    }
}
