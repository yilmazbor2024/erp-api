using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSoftware")]
    public partial class cdSoftware
    {
        public cdSoftware()
        {
            cdSoftwareDescs = new HashSet<cdSoftwareDesc>();
            prEmployeeSoftwares = new HashSet<prEmployeeSoftware>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SoftWareCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SoftWareTypeCode { get; set; }

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

        // Navigation Properties
        public virtual cdSoftwareType cdSoftwareType { get; set; }

        public virtual ICollection<cdSoftwareDesc> cdSoftwareDescs { get; set; }
        public virtual ICollection<prEmployeeSoftware> prEmployeeSoftwares { get; set; }
    }
}
