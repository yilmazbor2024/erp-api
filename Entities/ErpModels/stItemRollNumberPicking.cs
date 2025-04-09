using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stItemRollNumberPicking")]
    public partial class stItemRollNumberPicking
    {
        public stItemRollNumberPicking()
        {
        }

        [Key]
        [Required]
        public Guid ItemRollNumberPickingID { get; set; }

        public Guid? PickingLineID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RollNumber { get; set; }

        [Required]
        public double Qty { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchGroupCode { get; set; }

        // Navigation Properties
        public virtual cdBatchGroup cdBatchGroup { get; set; }
        public virtual cdBatch cdBatch { get; set; }
        public virtual cdRoll cdRoll { get; set; }
        public virtual trPickingLine trPickingLine { get; set; }

    }
}
