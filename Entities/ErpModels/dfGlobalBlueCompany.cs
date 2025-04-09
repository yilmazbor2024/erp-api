using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfGlobalBlueCompany")]
    public partial class dfGlobalBlueCompany
    {
        public dfGlobalBlueCompany()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ServiceUrl { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SenderID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ApiURL { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebsiteURL { get; set; }

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
