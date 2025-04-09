using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdInternationalUnitOfMeasure")]
    public partial class cdInternationalUnitOfMeasure
    {
        public cdInternationalUnitOfMeasure()
        {
            cdInternationalUnitOfMeasureDescs = new HashSet<cdInternationalUnitOfMeasureDesc>();
            cdUnitOfMeasures = new HashSet<cdUnitOfMeasure>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string InternationalUnitOfMeasureCode { get; set; }

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

        public virtual ICollection<cdInternationalUnitOfMeasureDesc> cdInternationalUnitOfMeasureDescs { get; set; }
        public virtual ICollection<cdUnitOfMeasure> cdUnitOfMeasures { get; set; }
    }
}
