using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDeviceType")]
    public partial class bsDeviceType
    {
        public bsDeviceType()
        {
            bsDeviceTypeDescs = new HashSet<bsDeviceTypeDesc>();
            dfPosDeviceParameterss = new HashSet<dfPosDeviceParameters>();
            prPaymentProviderConverts = new HashSet<prPaymentProviderConvert>();
            prPosTerminalDevices = new HashSet<prPosTerminalDevice>();
        }

        [Key]
        [Required]
        public byte DeviceTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDeviceTypeDesc> bsDeviceTypeDescs { get; set; }
        public virtual ICollection<dfPosDeviceParameters> dfPosDeviceParameterss { get; set; }
        public virtual ICollection<prPaymentProviderConvert> prPaymentProviderConverts { get; set; }
        public virtual ICollection<prPosTerminalDevice> prPosTerminalDevices { get; set; }
    }
}
