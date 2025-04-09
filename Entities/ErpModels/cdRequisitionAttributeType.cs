using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdRequisitionAttributeType")]
    public partial class cdRequisitionAttributeType
    {
        public cdRequisitionAttributeType()
        {
            cdRequisitionAttributes = new HashSet<cdRequisitionAttribute>();
            cdRequisitionAttributeTypeDescs = new HashSet<cdRequisitionAttributeTypeDesc>();
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

        public virtual ICollection<cdRequisitionAttribute> cdRequisitionAttributes { get; set; }
        public virtual ICollection<cdRequisitionAttributeTypeDesc> cdRequisitionAttributeTypeDescs { get; set; }
    }
}
