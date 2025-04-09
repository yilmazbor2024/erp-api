using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsQuery")]
    public partial class bsQuery
    {
        public bsQuery()
        {
        }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string QueryName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string KeyCodes { get; set; }

        public string QueryText { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

    }
}
