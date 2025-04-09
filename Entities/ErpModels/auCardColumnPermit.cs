using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auCardColumnPermit")]
    public partial class auCardColumnPermit
    {
        public auCardColumnPermit()
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

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ColumnName { get; set; }

        public bool? CanSelect { get; set; }

        public bool? CanUpdate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string DefaultValue { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdRole cdRole { get; set; }
    }
}
