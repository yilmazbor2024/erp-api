using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("xlIlceler")]
    public partial class xlIlceler
    {
        public xlIlceler()
        {
        }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IlceKodu { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string IlceAdi { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IlKodu { get; set; }

    }
}
