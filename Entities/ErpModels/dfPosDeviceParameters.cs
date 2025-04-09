using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfPosDeviceParameters")]
    public partial class dfPosDeviceParameters
    {
        public dfPosDeviceParameters()
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

        [Key]
        [Required]
        public byte DeviceCode { get; set; }

        [Required]
        public byte DeviceTypeCode { get; set; }

        [Required]
        public byte POSModeCode { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ParameterName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ParameterValue { get; set; }

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
        public virtual bsDevice bsDevice { get; set; }
        public virtual bsDeviceType bsDeviceType { get; set; }
        public virtual cdPOSTerminal cdPOSTerminal { get; set; }

    }
}
