using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdTextileCareSymbol")]
    public partial class cdTextileCareSymbol
    {
        public cdTextileCareSymbol()
        {
            cdTextileCareSymbolDescs = new HashSet<cdTextileCareSymbolDesc>();
            prItemTextileCareSymbols = new HashSet<prItemTextileCareSymbol>();
            prItemTextileCareTemplateSymbols = new HashSet<prItemTextileCareTemplateSymbol>();
        }

        [Key]
        [Required]
        public byte TextileCareSymbolGrCode { get; set; }

        [Key]
        [Required]
        public short TextileCareSymbolCode { get; set; }

        [Required]
        public byte[] TextileCareSymbolImage { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual bsTextileCareSymbolGr bsTextileCareSymbolGr { get; set; }

        public virtual ICollection<cdTextileCareSymbolDesc> cdTextileCareSymbolDescs { get; set; }
        public virtual ICollection<prItemTextileCareSymbol> prItemTextileCareSymbols { get; set; }
        public virtual ICollection<prItemTextileCareTemplateSymbol> prItemTextileCareTemplateSymbols { get; set; }
    }
}
