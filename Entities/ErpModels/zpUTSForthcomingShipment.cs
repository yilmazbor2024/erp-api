using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpUTSForthcomingShipment")]
    public partial class zpUTSForthcomingShipment
    {
        public zpUTSForthcomingShipment()
        {
        }

        [Key]
        [Required]
        public Guid UTSForthcomingShipmentID { get; set; }

        [Required]
        public Guid StockID { get; set; }

        [Required]
        public Guid BID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UNO { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UDI { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SNO { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LNO { get; set; }

        [Required]
        public DateTime URT { get; set; }

        [Required]
        public DateTime SKT { get; set; }

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
