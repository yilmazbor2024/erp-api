using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfProductGroupLevelForOfficeBasedSerialNumberTracking")]
    public partial class dfProductGroupLevelForOfficeBasedSerialNumberTracking
    {
        public dfProductGroupLevelForOfficeBasedSerialNumberTracking()
        {
        }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevel01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevel02 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevel03 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevel04 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevel05 { get; set; }

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
