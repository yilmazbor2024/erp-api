using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auReportQueryPermit")]
    public partial class auReportQueryPermit
    {
        public auReportQueryPermit()
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
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string QueryID { get; set; }

        [Required]
        public bool IsVisible { get; set; }

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
