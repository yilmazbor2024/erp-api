using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trOrderLineSum")]
    public partial class trOrderLineSum
    {
        public trOrderLineSum()
        {
            trOrderLineSumDetails = new HashSet<trOrderLineSumDetail>();
        }

        [Key]
        [Required]
        public Guid OrderHeaderID { get; set; }

        [Key]
        [Required]
        public int OrderLineSumID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LotCode { get; set; }

        [Required]
        public int LotQty { get; set; }

        public decimal? Price { get; set; }

        public decimal? PriceVI { get; set; }

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
        public virtual trOrderHeader trOrderHeader { get; set; }

        public virtual ICollection<trOrderLineSumDetail> trOrderLineSumDetails { get; set; }
    }
}
