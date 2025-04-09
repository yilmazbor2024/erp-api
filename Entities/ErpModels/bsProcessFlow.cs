using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsProcessFlow")]
    public partial class bsProcessFlow
    {
        public bsProcessFlow()
        {
            auProcessFlowDenys = new HashSet<auProcessFlowDeny>();
            auProcessPermits = new HashSet<auProcessPermit>();
            auProformaProcessPermits = new HashSet<auProformaProcessPermit>();
            bsProcessFlowDescs = new HashSet<bsProcessFlowDesc>();
            dfEArchiveOfficialForms = new HashSet<dfEArchiveOfficialForm>();
            dfEInvoiceOfficialForms = new HashSet<dfEInvoiceOfficialForm>();
            dfEShipmentOfficialForms = new HashSet<dfEShipmentOfficialForm>();
            dfProcessOfficialForms = new HashSet<dfProcessOfficialForm>();
            prITAttributeTypeRequiredProcessess = new HashSet<prITAttributeTypeRequiredProcesses>();
            srRefNumberProcessFlows = new HashSet<srRefNumberProcessFlow>();
            srSerialNumbers = new HashSet<srSerialNumber>();
        }

        [Key]
        [Required]
        public byte ProcessFlowCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<auProcessFlowDeny> auProcessFlowDenys { get; set; }
        public virtual ICollection<auProcessPermit> auProcessPermits { get; set; }
        public virtual ICollection<auProformaProcessPermit> auProformaProcessPermits { get; set; }
        public virtual ICollection<bsProcessFlowDesc> bsProcessFlowDescs { get; set; }
        public virtual ICollection<dfEArchiveOfficialForm> dfEArchiveOfficialForms { get; set; }
        public virtual ICollection<dfEInvoiceOfficialForm> dfEInvoiceOfficialForms { get; set; }
        public virtual ICollection<dfEShipmentOfficialForm> dfEShipmentOfficialForms { get; set; }
        public virtual ICollection<dfProcessOfficialForm> dfProcessOfficialForms { get; set; }
        public virtual ICollection<prITAttributeTypeRequiredProcesses> prITAttributeTypeRequiredProcessess { get; set; }
        public virtual ICollection<srRefNumberProcessFlow> srRefNumberProcessFlows { get; set; }
        public virtual ICollection<srSerialNumber> srSerialNumbers { get; set; }
    }
}
