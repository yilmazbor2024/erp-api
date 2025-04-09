using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prAvailableDeclarationPostTypes")]
    public partial class prAvailableDeclarationPostTypes
    {
        public prAvailableDeclarationPostTypes()
        {
        }

        [Key]
        [Required]
        public byte DeclarationTypeCode { get; set; }

        [Key]
        [Required]
        public byte TypeCode { get; set; }

        [Key]
        [Required]
        public byte DeclarationPostTypeCode { get; set; }

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
        public virtual bsDeclarationPostType bsDeclarationPostType { get; set; }
        public virtual bsDeclarationType bsDeclarationType { get; set; }

    }
}
