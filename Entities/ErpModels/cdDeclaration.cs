using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDeclaration")]
    public partial class cdDeclaration
    {
        public cdDeclaration()
        {
            cdDeclarationDescs = new HashSet<cdDeclarationDesc>();
            prDeclarationXMLs = new HashSet<prDeclarationXML>();
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

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public byte DeclarationCapacityCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AccountantCode1 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AccountantCode2 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AccountantCode3 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AccountantCode4 { get; set; }

        [Required]
        public decimal Assessment { get; set; }

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

        // Navigation Properties
        public virtual bsDeclarationCapacity bsDeclarationCapacity { get; set; }
        public virtual bsDeclarationType bsDeclarationType { get; set; }
        public virtual cdAccountant cdAccountant { get; set; }

        public virtual ICollection<cdDeclarationDesc> cdDeclarationDescs { get; set; }
        public virtual ICollection<prDeclarationXML> prDeclarationXMLs { get; set; }
    }
}
