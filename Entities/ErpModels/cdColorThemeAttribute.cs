using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdColorThemeAttribute")]
    public partial class cdColorThemeAttribute
    {
        public cdColorThemeAttribute()
        {
            cdColorThemeAttributeDescs = new HashSet<cdColorThemeAttributeDesc>();
            prColorThemeAttributes = new HashSet<prColorThemeAttribute>();
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
        public virtual cdColorThemeAttributeType cdColorThemeAttributeType { get; set; }

        public virtual ICollection<cdColorThemeAttributeDesc> cdColorThemeAttributeDescs { get; set; }
        public virtual ICollection<prColorThemeAttribute> prColorThemeAttributes { get; set; }
    }
}
