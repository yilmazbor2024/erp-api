using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_InboxShipmentLine")]
    public partial class e_InboxShipmentLine
    {
        public e_InboxShipmentLine()
        {
            e_InboxShipmentLineV3Itemss = new HashSet<e_InboxShipmentLineV3Items>();
        }

        [Key]
        [Required]
        public Guid InboxShipmentLineID { get; set; }

        [Required]
        public Guid UUID { get; set; }

        [Required]
        public int LineID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitCode { get; set; }

        [Required]
        public double Qty { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ItemName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SellersItemIdentificationID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BuyersItemIdentificationID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ManufacturersItemIdentificationID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CompanyBarcode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        // Navigation Properties
        public virtual e_InboxShipmentHeader e_InboxShipmentHeader { get; set; }

        public virtual ICollection<e_InboxShipmentLineV3Items> e_InboxShipmentLineV3Itemss { get; set; }
    }
}
