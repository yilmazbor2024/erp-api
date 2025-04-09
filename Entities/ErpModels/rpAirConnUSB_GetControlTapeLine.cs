using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpAirConnUSB_GetControlTapeLine")]
    public partial class rpAirConnUSB_GetControlTapeLine
    {
        public rpAirConnUSB_GetControlTapeLine()
        {
        }

        [Key]
        [Required]
        public Guid TransactionLineID { get; set; }

        [Required]
        public DateTime createdAtUtc { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string delivered { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string document_id { get; set; }

        [Required]
        public int item_count { get; set; }

        [Required]
        public int number { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string short_document_id { get; set; }

        [Required]
        public decimal total_sum { get; set; }

        [Required]
        public decimal total_vat { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string type { get; set; }

        [Required]
        public Guid TransactionID { get; set; }

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
        public virtual rpAirConnUSB_GetControlTapeHeader rpAirConnUSB_GetControlTapeHeader { get; set; }

    }
}
