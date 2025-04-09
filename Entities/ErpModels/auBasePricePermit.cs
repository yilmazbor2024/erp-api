using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auBasePricePermit")]
    public partial class auBasePricePermit
    {
        public auBasePricePermit()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RoleCode { get; set; }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [Required]
        public byte BasePriceCode { get; set; }

        [Required]
        public bool CanSelect { get; set; }

        [Required]
        public bool CanUpdate { get; set; }

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
        public virtual bsBasePrice bsBasePrice { get; set; }
        public virtual cdRole cdRole { get; set; }
        public virtual bsItemType bsItemType { get; set; }

    }
}
