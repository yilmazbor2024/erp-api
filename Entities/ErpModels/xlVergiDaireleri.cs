using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("xlVergiDaireleri")]
    public partial class xlVergiDaireleri
    {
        public xlVergiDaireleri()
        {
        }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VergiDairesiKodu { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string VergiDairesiAdi { get; set; }

    }
}
