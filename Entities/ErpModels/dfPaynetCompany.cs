using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfPaynetCompany")]
    public partial class dfPaynetCompany
    {
        public dfPaynetCompany()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ServiceAddress { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SecretKey { get; set; }

        [Required]
        public short ExpireDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaynetBankCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RatioCode { get; set; }

        [Required]
        public bool UsePaymentPlan { get; set; }

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
        public virtual cdBank cdBank { get; set; }

    }
}
