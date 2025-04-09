using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsQueryCustom")]
    public partial class bsQueryCustom
    {
        public bsQueryCustom()
        {
        }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string QueryName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string KeyCodes { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object AdvancedQueryOption { get; set; }

        public string QueryText { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

    }
}
