using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPayrollType")]
    public partial class cdPayrollType
    {
        public cdPayrollType()
        {
            cdPayrollTypeDescs = new HashSet<cdPayrollTypeDesc>();
            trPayrollHeaders = new HashSet<trPayrollHeader>();
        }

        [Key]
        [Required]
        public byte PayrollTypeCode { get; set; }

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

        public virtual ICollection<cdPayrollTypeDesc> cdPayrollTypeDescs { get; set; }
        public virtual ICollection<trPayrollHeader> trPayrollHeaders { get; set; }
    }
}
