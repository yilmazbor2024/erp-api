using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsNebimV3HotfixVersion")]
    public partial class bsNebimV3HotfixVersion
    {
        public bsNebimV3HotfixVersion()
        {
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Version { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string HotfixVersion { get; set; }

        [Required]
        public DateTime InstallationDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string InstallationUserName { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

    }
}
