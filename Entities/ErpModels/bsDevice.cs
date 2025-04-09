using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDevice")]
    public partial class bsDevice
    {
        public bsDevice()
        {
            bsDeviceDescs = new HashSet<bsDeviceDesc>();
            dfPosDeviceParameterss = new HashSet<dfPosDeviceParameters>();
            prPaymentProviderConverts = new HashSet<prPaymentProviderConvert>();
            prPosTerminalDevices = new HashSet<prPosTerminalDevice>();
        }

        [Key]
        [Required]
        public byte DeviceCode { get; set; }

        [Required]
        public byte DeviceTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClassLibraryName { get; set; }

        [Required]
        public bool IsOKC { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDeviceDesc> bsDeviceDescs { get; set; }
        public virtual ICollection<dfPosDeviceParameters> dfPosDeviceParameterss { get; set; }
        public virtual ICollection<prPaymentProviderConvert> prPaymentProviderConverts { get; set; }
        public virtual ICollection<prPosTerminalDevice> prPosTerminalDevices { get; set; }
    }
}
