using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfGetirCarsiOrderCancelReasonConvert")]
    public partial class dfGetirCarsiOrderCancelReasonConvert
    {
        public dfGetirCarsiOrderCancelReasonConvert()
        {
        }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string GetirCarsiID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string GetirCarsiIDDesc { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string OrderCancelReasonCode { get; set; }

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
        public virtual cdOrderCancelReason cdOrderCancelReason { get; set; }

    }
}
