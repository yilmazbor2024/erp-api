using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsTextileCareSymbolGr")]
    public partial class bsTextileCareSymbolGr
    {
        public bsTextileCareSymbolGr()
        {
            bsTextileCareSymbolGrDescs = new HashSet<bsTextileCareSymbolGrDesc>();
            cdTextileCareSymbols = new HashSet<cdTextileCareSymbol>();
        }

        [Key]
        [Required]
        public byte TextileCareSymbolGrCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsTextileCareSymbolGrDesc> bsTextileCareSymbolGrDescs { get; set; }
        public virtual ICollection<cdTextileCareSymbol> cdTextileCareSymbols { get; set; }
    }
}
