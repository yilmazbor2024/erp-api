using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prItemTextileCareTemplateSymbol")]
    public partial class prItemTextileCareTemplateSymbol
    {
        public prItemTextileCareTemplateSymbol()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemTextileCareTemplateCode { get; set; }

        [Key]
        [Required]
        public byte TextileCareSymbolGrCode { get; set; }

        [Key]
        [Required]
        public short TextileCareSymbolCode { get; set; }

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
        public virtual cdTextileCareSymbol cdTextileCareSymbol { get; set; }
        public virtual cdItemTextileCareTemplate cdItemTextileCareTemplate { get; set; }

    }
}
