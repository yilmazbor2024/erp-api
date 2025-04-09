using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prPosTerminalDevice")]
    public partial class prPosTerminalDevice
    {
        public prPosTerminalDevice()
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

        [Key]
        [Required]
        public byte DeviceTypeCode { get; set; }

        [Required]
        public byte DeviceCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual bsDeviceType bsDeviceType { get; set; }
        public virtual bsDevice bsDevice { get; set; }
        public virtual cdPOSTerminal cdPOSTerminal { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
