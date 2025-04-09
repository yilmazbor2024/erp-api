using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpUmicoTransaction")]
    public partial class zpUmicoTransaction
    {
        public zpUmicoTransaction()
        {
        }

        [Key]
        [Required]
        public Guid UmicoTransactionID { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string order_ext_id { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string refund_ext_id { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string loy_card_number { get; set; }

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

    }
}
