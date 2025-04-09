using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsInternetPaymentType")]
    public partial class bsInternetPaymentType
    {
        public bsInternetPaymentType()
        {
            dfOnlineSalesandPaymentParameterss = new HashSet<dfOnlineSalesandPaymentParameters>();
            tpOrdersViaInternetInfos = new HashSet<tpOrdersViaInternetInfo>();
            tpSalesViaInternetInfos = new HashSet<tpSalesViaInternetInfo>();
        }

        [Key]
        [Required]
        public byte PaymentTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<dfOnlineSalesandPaymentParameters> dfOnlineSalesandPaymentParameterss { get; set; }
        public virtual ICollection<tpOrdersViaInternetInfo> tpOrdersViaInternetInfos { get; set; }
        public virtual ICollection<tpSalesViaInternetInfo> tpSalesViaInternetInfos { get; set; }
    }
}
