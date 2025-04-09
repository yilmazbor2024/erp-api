using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfTaxFreePointCompany")]
    public partial class dfTaxFreePointCompany
    {
        public dfTaxFreePointCompany()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CorporateID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ServiceURL { get; set; }

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
