using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prV3ToMdmIDMap")]
    public partial class prV3ToMdmIDMap
    {
        public prV3ToMdmIDMap()
        {
        }

        [Key]
        [Required]
        public int EntityType { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string V3ID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MDMID { get; set; }

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

    }
}
