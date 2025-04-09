using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsNebimV3Version")]
    public partial class bsNebimV3Version
    {
        public bsNebimV3Version()
        {
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Version { get; set; }

        [Required]
        public DateTime InstallationDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string InstallationUserName { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

    }
}
