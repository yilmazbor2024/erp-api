using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsMT940Process")]
    public partial class bsMT940Process
    {
        public bsMT940Process()
        {
            prMT940ProcessRuless = new HashSet<prMT940ProcessRules>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MT940ProcessCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MT940ProcessDescription { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<prMT940ProcessRules> prMT940ProcessRuless { get; set; }
    }
}
