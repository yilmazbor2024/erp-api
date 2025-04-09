using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPDCQueryList")]
    public partial class bsPDCQueryList
    {
        public bsPDCQueryList()
        {
        }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string QueryName { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

    }
}
