using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdOrderCancelReason")]
    public partial class cdOrderCancelReason
    {
        public cdOrderCancelReason()
        {
            cdOrderCancelReasonDescs = new HashSet<cdOrderCancelReasonDesc>();
            dfGetirCarsiOrderCancelReasonConverts = new HashSet<dfGetirCarsiOrderCancelReasonConvert>();
            dfOnlineSalesandPaymentParameterss = new HashSet<dfOnlineSalesandPaymentParameters>();
            tpOrderCancelDetails = new HashSet<tpOrderCancelDetail>();
            trInnerOrderLines = new HashSet<trInnerOrderLine>();
            trOrderLines = new HashSet<trOrderLine>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string OrderCancelReasonCode { get; set; }

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

        public virtual ICollection<cdOrderCancelReasonDesc> cdOrderCancelReasonDescs { get; set; }
        public virtual ICollection<dfGetirCarsiOrderCancelReasonConvert> dfGetirCarsiOrderCancelReasonConverts { get; set; }
        public virtual ICollection<dfOnlineSalesandPaymentParameters> dfOnlineSalesandPaymentParameterss { get; set; }
        public virtual ICollection<tpOrderCancelDetail> tpOrderCancelDetails { get; set; }
        public virtual ICollection<trInnerOrderLine> trInnerOrderLines { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
    }
}
