using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDeclarationTypeDetail")]
    public partial class prDeclarationTypeDetail
    {
        public prDeclarationTypeDetail()
        {
        }

        [Key]
        [Required]
        public byte DeclarationTypeCode { get; set; }

        [Key]
        [Required]
        public byte TypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TypeDescription { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LineCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string GroupDescription { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

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
        public virtual bsDeclarationType bsDeclarationType { get; set; }

    }
}
