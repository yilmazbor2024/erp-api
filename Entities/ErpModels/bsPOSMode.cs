using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPOSMode")]
    public partial class bsPOSMode
    {
        public bsPOSMode()
        {
            bsPOSModeDescs = new HashSet<bsPOSModeDesc>();
        }

        [Key]
        [Required]
        public byte POSModeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClassLibraryName { get; set; }

        [Required]
        public bool IsOKC { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPOSModeDesc> bsPOSModeDescs { get; set; }
    }
}
