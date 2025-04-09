using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDeclarationType")]
    public partial class bsDeclarationType
    {
        public bsDeclarationType()
        {
            bsDeclarationTypeDescs = new HashSet<bsDeclarationTypeDesc>();
            cdDeclarations = new HashSet<cdDeclaration>();
            prAvailableDeclarationPostTypess = new HashSet<prAvailableDeclarationPostTypes>();
            prDeclarationGLAccss = new HashSet<prDeclarationGLAccs>();
            prDeclarationTypeDetails = new HashSet<prDeclarationTypeDetail>();
        }

        [Key]
        [Required]
        public byte DeclarationTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDeclarationTypeDesc> bsDeclarationTypeDescs { get; set; }
        public virtual ICollection<cdDeclaration> cdDeclarations { get; set; }
        public virtual ICollection<prAvailableDeclarationPostTypes> prAvailableDeclarationPostTypess { get; set; }
        public virtual ICollection<prDeclarationGLAccs> prDeclarationGLAccss { get; set; }
        public virtual ICollection<prDeclarationTypeDetail> prDeclarationTypeDetails { get; set; }
    }
}
