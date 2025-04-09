using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfOfficeNotAvailableProcess")]
    public partial class dfOfficeNotAvailableProcess
    {
        public dfOfficeNotAvailableProcess()
        {
        }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

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
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdOffice cdOffice { get; set; }

    }
}
