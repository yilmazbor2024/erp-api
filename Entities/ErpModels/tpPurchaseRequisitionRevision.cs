using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPurchaseRequisitionRevision")]
    public partial class tpPurchaseRequisitionRevision
    {
        public tpPurchaseRequisitionRevision()
        {
            rpPurchaseRequisitionConfirmationHistorys = new HashSet<rpPurchaseRequisitionConfirmationHistory>();
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionLineID { get; set; }

        [Key]
        [Required]
        public byte RevisionNumber { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string RevisionDescription { get; set; }

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

        // Navigation Properties
        public virtual trPurchaseRequisitionLine trPurchaseRequisitionLine { get; set; }

        public virtual ICollection<rpPurchaseRequisitionConfirmationHistory> rpPurchaseRequisitionConfirmationHistorys { get; set; }
    }
}
