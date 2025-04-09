using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsReconciliationType")]
    public partial class bsReconciliationType
    {
        public bsReconciliationType()
        {
            bsReconciliationTypeDescs = new HashSet<bsReconciliationTypeDesc>();
            cdReconciliations = new HashSet<cdReconciliation>();
        }

        [Key]
        [Required]
        public byte ReconciliationTypeCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SPName { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsCurrAccType bsCurrAccType { get; set; }

        public virtual ICollection<bsReconciliationTypeDesc> bsReconciliationTypeDescs { get; set; }
        public virtual ICollection<cdReconciliation> cdReconciliations { get; set; }
    }
}
