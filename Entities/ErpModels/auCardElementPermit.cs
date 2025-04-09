using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auCardElementPermit")]
    public partial class auCardElementPermit
    {
        public auCardElementPermit()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RoleCode { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CardName { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ElementCardName { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SubElementCardName1 { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SubElementCardName2 { get; set; }

        [Required]
        public bool IsReport { get; set; }

        public bool? CanSelect { get; set; }

        public bool? CanInsert { get; set; }

        public bool? CanUpdate { get; set; }

        public bool? CanDelete { get; set; }

        public bool? IsRequired { get; set; }

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
        public virtual cdRole cdRole { get; set; }
        public virtual cdCompany cdCompany { get; set; }
    }
}
