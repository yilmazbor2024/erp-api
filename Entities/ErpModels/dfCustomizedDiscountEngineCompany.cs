using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfCustomizedDiscountEngineCompany")]
    public partial class dfCustomizedDiscountEngineCompany
    {
        public dfCustomizedDiscountEngineCompany()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ServiceAddress { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Username { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Password { get; set; }

        [Required]
        public byte ITAttributeTypeCode { get; set; }

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
