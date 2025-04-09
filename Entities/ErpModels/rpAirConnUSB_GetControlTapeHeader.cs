using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpAirConnUSB_GetControlTapeHeader")]
    public partial class rpAirConnUSB_GetControlTapeHeader
    {
        public rpAirConnUSB_GetControlTapeHeader()
        {
            rpAirConnUSB_GetControlTapeLines = new HashSet<rpAirConnUSB_GetControlTapeLine>();
        }

        [Key]
        [Required]
        public Guid TransactionID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public byte PosTerminalID { get; set; }

        [Required]
        public DateTime createdAtUtc { get; set; }

        [Required]
        public int documents_quantity { get; set; }

        [Required]
        public DateTime shiftCloseAtUtc { get; set; }

        [Required]
        public DateTime shiftOpenAtUtc { get; set; }

        [Required]
        public int shift_number { get; set; }

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

        public virtual ICollection<rpAirConnUSB_GetControlTapeLine> rpAirConnUSB_GetControlTapeLines { get; set; }
    }
}
