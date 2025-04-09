using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdITAttributeType")]
    public partial class cdITAttributeType
    {
        public cdITAttributeType()
        {
            cdITAttributes = new HashSet<cdITAttribute>();
            cdITAttributeTypeDescs = new HashSet<cdITAttributeTypeDesc>();
            prITAttributeTypeRequiredProcessess = new HashSet<prITAttributeTypeRequiredProcesses>();
        }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Required]
        public bool UseReports { get; set; }

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

        public virtual ICollection<cdITAttribute> cdITAttributes { get; set; }
        public virtual ICollection<cdITAttributeTypeDesc> cdITAttributeTypeDescs { get; set; }
        public virtual ICollection<prITAttributeTypeRequiredProcesses> prITAttributeTypeRequiredProcessess { get; set; }
    }
}
