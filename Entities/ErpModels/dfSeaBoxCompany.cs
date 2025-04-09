using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfSeaBoxCompany")]
    public partial class dfSeaBoxCompany
    {
        public dfSeaBoxCompany()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ApiUrl { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string UserName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Password { get; set; }

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

    }
}
