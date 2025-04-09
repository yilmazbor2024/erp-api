using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsInnerProcess")]
    public partial class bsInnerProcess
    {
        public bsInnerProcess()
        {
            auInnerProcessPermits = new HashSet<auInnerProcessPermit>();
            bsInnerProcessDescs = new HashSet<bsInnerProcessDesc>();
            dfInnerOrderProcessOfficialForms = new HashSet<dfInnerOrderProcessOfficialForm>();
            dfInnerProcessOfficialForms = new HashSet<dfInnerProcessOfficialForm>();
            prInnerProcessInfos = new HashSet<prInnerProcessInfo>();
            prInnerProcessITAttributes = new HashSet<prInnerProcessITAttribute>();
            prInnerProcessItemTypes = new HashSet<prInnerProcessItemType>();
            prITAttributeTypeRequiredProcessess = new HashSet<prITAttributeTypeRequiredProcesses>();
            srRefNumberInnerOrders = new HashSet<srRefNumberInnerOrder>();
            srRefNumberInnerProcesss = new HashSet<srRefNumberInnerProcess>();
            trAdjustCostHeaders = new HashSet<trAdjustCostHeader>();
            trInnerHeaders = new HashSet<trInnerHeader>();
            trInnerOrderHeaders = new HashSet<trInnerOrderHeader>();
            trStocks = new HashSet<trStock>();
        }

        [Key]
        [Required]
        public object InnerProcessCode { get; set; }

        [Required]
        public byte TransTypeCode { get; set; }

        [Required]
        public bool IsOutOfCosting { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsTransType bsTransType { get; set; }

        public virtual ICollection<auInnerProcessPermit> auInnerProcessPermits { get; set; }
        public virtual ICollection<bsInnerProcessDesc> bsInnerProcessDescs { get; set; }
        public virtual ICollection<dfInnerOrderProcessOfficialForm> dfInnerOrderProcessOfficialForms { get; set; }
        public virtual ICollection<dfInnerProcessOfficialForm> dfInnerProcessOfficialForms { get; set; }
        public virtual ICollection<prInnerProcessInfo> prInnerProcessInfos { get; set; }
        public virtual ICollection<prInnerProcessITAttribute> prInnerProcessITAttributes { get; set; }
        public virtual ICollection<prInnerProcessItemType> prInnerProcessItemTypes { get; set; }
        public virtual ICollection<prITAttributeTypeRequiredProcesses> prITAttributeTypeRequiredProcessess { get; set; }
        public virtual ICollection<srRefNumberInnerOrder> srRefNumberInnerOrders { get; set; }
        public virtual ICollection<srRefNumberInnerProcess> srRefNumberInnerProcesss { get; set; }
        public virtual ICollection<trAdjustCostHeader> trAdjustCostHeaders { get; set; }
        public virtual ICollection<trInnerHeader> trInnerHeaders { get; set; }
        public virtual ICollection<trInnerOrderHeader> trInnerOrderHeaders { get; set; }
        public virtual ICollection<trStock> trStocks { get; set; }
    }
}
