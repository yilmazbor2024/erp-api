using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prPaymentProviderConvert")]
    public partial class prPaymentProviderConvert
    {
        public prPaymentProviderConvert()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentProviderCode { get; set; }

        [Required]
        public byte POSModeCode { get; set; }

        [Key]
        [Required]
        public byte DeviceCode { get; set; }

        [Required]
        public byte DeviceTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FiscalDevicePaymentType { get; set; }

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
        public virtual cdPaymentProvider cdPaymentProvider { get; set; }
        public virtual bsDeviceType bsDeviceType { get; set; }
        public virtual bsDevice bsDevice { get; set; }

    }
}
