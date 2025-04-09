using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfOfflinePosTerminalParameters")]
    public partial class dfOfflinePosTerminalParameters
    {
        public dfOfflinePosTerminalParameters()
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
        public short PosTerminalID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ServerUrl { get; set; }

        [Required]
        public short RequestTimeOut { get; set; }

        [Required]
        public short HeartbeatTimeOut { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ImportFolder { get; set; }

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
