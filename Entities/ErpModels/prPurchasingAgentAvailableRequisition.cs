using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prPurchasingAgentAvailableRequisition")]
    public partial class prPurchasingAgentAvailableRequisition
    {
        public prPurchasingAgentAvailableRequisition()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PurchasingAgentCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RequisitionTypeCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RequisitionCode { get; set; }

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
        public virtual cdRequisition cdRequisition { get; set; }
        public virtual cdRequisitionType cdRequisitionType { get; set; }
        public virtual cdCompany cdCompany { get; set; }

    }
}
