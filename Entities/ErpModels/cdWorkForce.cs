using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdWorkForce")]
    public partial class cdWorkForce
    {
        public cdWorkForce()
        {
            cdWorkForceDescs = new HashSet<cdWorkForceDesc>();
            prEmployeeWorkplaceInformations = new HashSet<prEmployeeWorkplaceInformation>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkForceCode { get; set; }

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

        public virtual ICollection<cdWorkForceDesc> cdWorkForceDescs { get; set; }
        public virtual ICollection<prEmployeeWorkplaceInformation> prEmployeeWorkplaceInformations { get; set; }
    }
}
