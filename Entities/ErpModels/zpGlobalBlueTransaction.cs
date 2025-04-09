using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpGlobalBlueTransaction")]
    public partial class zpGlobalBlueTransaction
    {
        public zpGlobalBlueTransaction()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DocID { get; set; }

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
