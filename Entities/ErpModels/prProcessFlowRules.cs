using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prProcessFlowRules")]
    public partial class prProcessFlowRules
    {
        public prProcessFlowRules()
        {
        }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProcessFlowOrder { get; set; }

        [Required]
        public bool IsPackageShippingConfirmationActive { get; set; }

        [Required]
        public bool IsDispOrderConfirmationActive { get; set; }

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
        public virtual bsProcess bsProcess { get; set; }

    }
}
