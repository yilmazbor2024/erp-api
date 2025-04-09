using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prConsentSourceConvert")]
    public partial class prConsentSourceConvert
    {
        public prConsentSourceConvert()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Source { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PermissionMarketingServiceCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ConvertedSource { get; set; }

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
        public virtual bsConsentSource bsConsentSource { get; set; }

    }
}
