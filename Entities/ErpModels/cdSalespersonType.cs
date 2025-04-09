using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSalespersonType")]
    public partial class cdSalespersonType
    {
        public cdSalespersonType()
        {
            cdSalespersons = new HashSet<cdSalesperson>();
            cdSalespersonTypeDescs = new HashSet<cdSalespersonTypeDesc>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonTypeCode { get; set; }

        [Required]
        public float BasePremiumRate { get; set; }

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
        public virtual ICollection<cdSalespersonTypeDesc> cdSalespersonTypeDescs { get; set; }
    }
}
