using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auCustomizedDiscountEngineServiceGetReturnableItemsLog")]
    public partial class auCustomizedDiscountEngineServiceGetReturnableItemsLog
    {
        public auCustomizedDiscountEngineServiceGetReturnableItemsLog()
        {
        }

        [Key]
        [Required]
        public Guid GetReturnableItemsLogID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Required]
        public Guid InvoiceLineID { get; set; }

        [Required]
        public double Qty1 { get; set; }

        public string Message { get; set; }

        public string DiscountOfferDescription { get; set; }

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
