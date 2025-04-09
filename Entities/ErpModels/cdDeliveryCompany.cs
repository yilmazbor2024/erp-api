using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDeliveryCompany")]
    public partial class cdDeliveryCompany
    {
        public cdDeliveryCompany()
        {
            cdDeliveryCompanyDescs = new HashSet<cdDeliveryCompanyDesc>();
            dfOnlineSalesandPaymentParameterss = new HashSet<dfOnlineSalesandPaymentParameters>();
            prDeliveryCompanyMarketPlaceMappings = new HashSet<prDeliveryCompanyMarketPlaceMapping>();
            tpInnerHeaderExtensions = new HashSet<tpInnerHeaderExtension>();
            trInnerOrderHeaders = new HashSet<trInnerOrderHeader>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
            trVehicleLoadingHeaders = new HashSet<trVehicleLoadingHeader>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryCompanyCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<cdDeliveryCompanyDesc> cdDeliveryCompanyDescs { get; set; }
        public virtual ICollection<dfOnlineSalesandPaymentParameters> dfOnlineSalesandPaymentParameterss { get; set; }
        public virtual ICollection<prDeliveryCompanyMarketPlaceMapping> prDeliveryCompanyMarketPlaceMappings { get; set; }
        public virtual ICollection<tpInnerHeaderExtension> tpInnerHeaderExtensions { get; set; }
        public virtual ICollection<trInnerOrderHeader> trInnerOrderHeaders { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
        public virtual ICollection<trVehicleLoadingHeader> trVehicleLoadingHeaders { get; set; }
    }
}
