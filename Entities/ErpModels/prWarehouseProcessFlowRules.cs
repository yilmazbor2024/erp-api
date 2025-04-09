using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prWarehouseProcessFlowRules")]
    public partial class prWarehouseProcessFlowRules
    {
        public prWarehouseProcessFlowRules()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

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
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }

    }
}
