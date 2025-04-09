using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPurchaseRequisitionTechnicalNotes")]
    public partial class tpPurchaseRequisitionTechnicalNotes
    {
        public tpPurchaseRequisitionTechnicalNotes()
        {
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionTechnicalNoteID { get; set; }

        [Required]
        public Guid PurchaseRequisitionLineID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

        public string Notes { get; set; }

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

    }
}
