using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDeclarationPostType")]
    public partial class bsDeclarationPostType
    {
        public bsDeclarationPostType()
        {
            prAvailableDeclarationPostTypess = new HashSet<prAvailableDeclarationPostTypes>();
            prDeclarationGLAccss = new HashSet<prDeclarationGLAccs>();
        }

        [Key]
        [Required]
        public byte DeclarationPostTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Description { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<prAvailableDeclarationPostTypes> prAvailableDeclarationPostTypess { get; set; }
        public virtual ICollection<prDeclarationGLAccs> prDeclarationGLAccss { get; set; }
    }
}
