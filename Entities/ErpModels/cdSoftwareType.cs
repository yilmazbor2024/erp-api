using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSoftwareType")]
    public partial class cdSoftwareType
    {
        public cdSoftwareType()
        {
            cdSoftwares = new HashSet<cdSoftware>();
            cdSoftwareTypeDescs = new HashSet<cdSoftwareTypeDesc>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SoftwareTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        public virtual ICollection<cdSoftware> cdSoftwares { get; set; }
        public virtual ICollection<cdSoftwareTypeDesc> cdSoftwareTypeDescs { get; set; }
    }
}
