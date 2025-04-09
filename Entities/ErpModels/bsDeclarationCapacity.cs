using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDeclarationCapacity")]
    public partial class bsDeclarationCapacity
    {
        public bsDeclarationCapacity()
        {
            bsDeclarationCapacityDescs = new HashSet<bsDeclarationCapacityDesc>();
            cdDeclarations = new HashSet<cdDeclaration>();
        }

        [Key]
        [Required]
        public byte DeclarationCapacityCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDeclarationCapacityDesc> bsDeclarationCapacityDescs { get; set; }
        public virtual ICollection<cdDeclaration> cdDeclarations { get; set; }
    }
}
