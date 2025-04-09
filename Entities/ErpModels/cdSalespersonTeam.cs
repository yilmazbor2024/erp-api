using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSalespersonTeam")]
    public partial class cdSalespersonTeam
    {
        public cdSalespersonTeam()
        {
            cdSalespersons = new HashSet<cdSalesperson>();
            cdSalespersonTeamDescs = new HashSet<cdSalespersonTeamDesc>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonTeamCode { get; set; }

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

        public virtual ICollection<cdSalesperson> cdSalespersons { get; set; }
        public virtual ICollection<cdSalespersonTeamDesc> cdSalespersonTeamDescs { get; set; }
    }
}
