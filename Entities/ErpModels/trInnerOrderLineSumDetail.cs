using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInnerOrderLineSumDetail")]
    public partial class trInnerOrderLineSumDetail
    {
        public trInnerOrderLineSumDetail()
        {
        }

        [Key]
        [Required]
        public Guid InnerOrderHeaderID { get; set; }
 
        [Key]
        [Required]
        public int InnerOrderLineSumID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LotCode { get; set; }

        [Required]
        public int LotQty { get; set; }

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
        public virtual cdLot cdLot { get; set; }
        public virtual trInnerOrderLineSum trInnerOrderLineSum { get; set; }
        public virtual trInnerOrderHeader trInnerOrderHeader { get; set; }
        public virtual cdColor cdColor { get; set; }

    }
}
