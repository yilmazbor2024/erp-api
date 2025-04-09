using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsUTSDeclarationField")]
    public partial class bsUTSDeclarationField
    {
        public bsUTSDeclarationField()
        {
        }

        [Key]
        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Field { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

    }
}
