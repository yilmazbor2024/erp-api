using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfUmicoPOSTerminal")]
    public partial class dfUmicoPOSTerminal
    {
        public dfUmicoPOSTerminal()
        {
        }

        [Key]
        [Required]
        public byte StoreTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Key]
        [Required]
        public short POSTerminalID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Pos_ext_id { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BarcodeTypeCode { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdPOSTerminal cdPOSTerminal { get; set; }

    }
}
