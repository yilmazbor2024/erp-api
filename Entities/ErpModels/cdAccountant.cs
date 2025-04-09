using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdAccountant")]
    public partial class cdAccountant
    {
        public cdAccountant()
        {
            cdAccountantDescs = new HashSet<cdAccountantDesc>();
            cdDeclarations = new HashSet<cdDeclaration>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AccountantCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirstName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LastName { get; set; }

        public string FirstLastName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TaxNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StampNum { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CommercialRegistrationNo { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMail { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Tel { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string MobileTel { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Fax { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<cdAccountantDesc> cdAccountantDescs { get; set; }
        public virtual ICollection<cdDeclaration> cdDeclarations { get; set; }
    }
}
