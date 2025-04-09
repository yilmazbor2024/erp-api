using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPolicyCustomerSharingDesc")]
    public partial class bsPolicyCustomerSharingDesc
    {
        public bsPolicyCustomerSharingDesc()
        {
        }

        [Key]
        [Required]
        public byte PolicyCustomerSharing { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PolicyCustomerSharingDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual bsPolicyCustomerSharing bsPolicyCustomerSharing { get; set; }

    }
}
