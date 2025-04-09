using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsMessageImportance")]
    public partial class bsMessageImportance
    {
        public bsMessageImportance()
        {
            bsMessageImportanceDescs = new HashSet<bsMessageImportanceDesc>();
            trMessageBoxs = new HashSet<trMessageBox>();
        }

        [Key]
        [Required]
        public byte MessageImportanceCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsMessageImportanceDesc> bsMessageImportanceDescs { get; set; }
        public virtual ICollection<trMessageBox> trMessageBoxs { get; set; }
    }
}
