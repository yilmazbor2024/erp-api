using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prConfirmationRequiredProductGroups")]
    public partial class prConfirmationRequiredProductGroups
    {
        public prConfirmationRequiredProductGroups()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductGroupCode { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Code { get; set; }

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

    }
}
