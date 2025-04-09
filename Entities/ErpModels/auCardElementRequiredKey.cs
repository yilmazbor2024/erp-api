using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auCardElementRequiredKey")]
    public partial class auCardElementRequiredKey
    {
        public auCardElementRequiredKey()
        {
        }

        [Required]
        public object CompanyCode { get; set; }

        [Required]

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RoleCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CardName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ElementCardName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SubElementCardName1 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SubElementCardName2 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RequiredKeyName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RequiredKeyCode { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdRole cdRole { get; set; }

    }
}
