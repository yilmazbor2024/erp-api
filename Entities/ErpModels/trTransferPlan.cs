using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trTransferPlan")]
    public partial class trTransferPlan
    {
        public trTransferPlan()
        {
            tpTransferPlanATAttributes = new HashSet<tpTransferPlanATAttribute>();
            tpTransferPlanITAttributes = new HashSet<tpTransferPlanITAttribute>();
            trTransferPlanChannels = new HashSet<trTransferPlanChannel>();
            trTransferPlanParameterValues = new HashSet<trTransferPlanParameterValue>();
            trTransferPlanProducts = new HashSet<trTransferPlanProduct>();
            trTransferPlanProductQtys = new HashSet<trTransferPlanProductQty>();
        }

        [Key]
        [Required]
        public Guid TransferPlanID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object TransferPlanNumber { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TransferPlanRuleCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ParameteredFieldsValue { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsCalculated { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedUserName { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [Required]
        public bool IsPostingOrder { get; set; }

        public string ProductFilterStringSQL { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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

        // Navigation Properties
        public virtual bsTransferPlanRule bsTransferPlanRule { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<tpTransferPlanATAttribute> tpTransferPlanATAttributes { get; set; }
        public virtual ICollection<tpTransferPlanITAttribute> tpTransferPlanITAttributes { get; set; }
        public virtual ICollection<trTransferPlanChannel> trTransferPlanChannels { get; set; }
        public virtual ICollection<trTransferPlanParameterValue> trTransferPlanParameterValues { get; set; }
        public virtual ICollection<trTransferPlanProduct> trTransferPlanProducts { get; set; }
        public virtual ICollection<trTransferPlanProductQty> trTransferPlanProductQtys { get; set; }
    }
}
