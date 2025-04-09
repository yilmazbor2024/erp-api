using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfIyzicoCPCompany")]
    public partial class dfIyzicoCPCompany
    {
        public dfIyzicoCPCompany()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Apikey { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SecretKey { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CPApikey { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CPSecretKey { get; set; }

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
