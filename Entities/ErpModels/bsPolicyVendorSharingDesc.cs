using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPolicyVendorSharingDesc")]
    public partial class bsPolicyVendorSharingDesc
    {
        public bsPolicyVendorSharingDesc()
        {
        }

        [Key]
        [Required]
        public byte PolicyVendorSharing { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PolicyVendorSharingDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual bsPolicyVendorSharing bsPolicyVendorSharing { get; set; }

    }
}
