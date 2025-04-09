using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDiagnostic")]
    public partial class cdDiagnostic
    {
        public cdDiagnostic()
        {
            cdDiagnosticDescs = new HashSet<cdDiagnosticDesc>();
            trOrderOpticalProducts = new HashSet<trOrderOpticalProduct>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string DiagnosticCode { get; set; }

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

        public virtual ICollection<cdDiagnosticDesc> cdDiagnosticDescs { get; set; }
        public virtual ICollection<trOrderOpticalProduct> trOrderOpticalProducts { get; set; }
    }
}
