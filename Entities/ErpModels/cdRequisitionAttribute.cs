using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdRequisitionAttribute")]
    public partial class cdRequisitionAttribute
    {
        public cdRequisitionAttribute()
        {
            cdRequisitionAttributeDescs = new HashSet<cdRequisitionAttributeDesc>();
            prRequisitionAttributes = new HashSet<prRequisitionAttribute>();
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
        public virtual cdRequisitionAttributeType cdRequisitionAttributeType { get; set; }

        public virtual ICollection<cdRequisitionAttributeDesc> cdRequisitionAttributeDescs { get; set; }
        public virtual ICollection<prRequisitionAttribute> prRequisitionAttributes { get; set; }
    }
}
