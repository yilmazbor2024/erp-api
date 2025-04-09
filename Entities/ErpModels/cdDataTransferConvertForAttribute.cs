using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDataTransferConvertForAttribute")]
    public partial class cdDataTransferConvertForAttribute
    {
        public cdDataTransferConvertForAttribute()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ConvertTypeCode { get; set; }

        [Key]
        [Required]
        public byte ExternalAttributeType { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExternalCode { get; set; }

        [Required]
        public byte InternalAttributeType { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string InternalCode { get; set; }

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
        public virtual bsDataTransferConvertType bsDataTransferConvertType { get; set; }

    }
}
