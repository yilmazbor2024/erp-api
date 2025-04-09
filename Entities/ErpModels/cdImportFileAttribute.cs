using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdImportFileAttribute")]
    public partial class cdImportFileAttribute
    {
        public cdImportFileAttribute()
        {
            cdImportFileAttributeDescs = new HashSet<cdImportFileAttributeDesc>();
            prImportFileAttributes = new HashSet<prImportFileAttribute>();
        }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttributeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

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
        public virtual cdImportFileAttributeType cdImportFileAttributeType { get; set; }

        public virtual ICollection<cdImportFileAttributeDesc> cdImportFileAttributeDescs { get; set; }
        public virtual ICollection<prImportFileAttribute> prImportFileAttributes { get; set; }
    }
}
