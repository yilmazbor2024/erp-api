using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfIncomeTaxRelief")]
    public partial class dfIncomeTaxRelief
    {
        public dfIncomeTaxRelief()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public short ValidYear { get; set; }

        [Key]
        [Required]
        public byte ValidMonth { get; set; }

        [Key]
        [Required]
        public byte EmployeeTaxStatusCode { get; set; }

        [Required]
        public float TaxReliefAmount { get; set; }

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
        public virtual cdEmployeeTaxStatus cdEmployeeTaxStatus { get; set; }

    }
}
