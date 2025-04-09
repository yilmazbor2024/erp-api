using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prInnerProcessItemType")]
    public partial class prInnerProcessItemType
    {
        public prInnerProcessItemType()
        {
        }

        [Key]
        [Required]
        public object InnerProcessCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

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
        public virtual bsInnerProcess bsInnerProcess { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsItemType bsItemType { get; set; }

    }
}
