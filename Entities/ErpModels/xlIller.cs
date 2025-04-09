using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("xlIller")]
    public partial class xlIller
    {
        public xlIller()
        {
        }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IlKodu { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string IlAdi { get; set; }

    }
}
