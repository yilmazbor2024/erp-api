using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInnerOrderLineSum")]
    public partial class trInnerOrderLineSum
    {
        public trInnerOrderLineSum()
        {
            trInnerOrderLineSumDetails = new HashSet<trInnerOrderLineSumDetail>();
        }

        [Key]
        [Required]
        public Guid InnerOrderHeaderId { get; set; }

        [Key]
        [Required]
        public int InnerOrderLineSumID { get; set; }

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
        public virtual trInnerOrderHeader trInnerOrderHeader { get; set; }

        public virtual ICollection<trInnerOrderLineSumDetail> trInnerOrderLineSumDetails { get; set; }
    }
}
