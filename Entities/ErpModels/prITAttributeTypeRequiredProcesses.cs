using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prITAttributeTypeRequiredProcesses")]
    public partial class prITAttributeTypeRequiredProcesses
    {
        public prITAttributeTypeRequiredProcesses()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [Key]
        [Required]
        public object InnerProcessCode { get; set; }

        [Key]
        [Required]
        public byte ProcessFlowCode { get; set; }

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
        public virtual bsProcessFlow bsProcessFlow { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual bsInnerProcess bsInnerProcess { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdITAttributeType cdITAttributeType { get; set; }

    }
}
