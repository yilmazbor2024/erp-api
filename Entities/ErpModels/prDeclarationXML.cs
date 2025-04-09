using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDeclarationXML")]
    public partial class prDeclarationXML
    {
        public prDeclarationXML()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DeclarationCode { get; set; }

        [Key]
        [Required]
        public byte DeclarationTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string XmlName { get; set; }

        [Required]
        public object XmlFile { get; set; }

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
        public virtual cdDeclaration cdDeclaration { get; set; }

    }
}
