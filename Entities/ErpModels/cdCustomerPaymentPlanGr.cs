using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCustomerPaymentPlanGr")]
    public partial class cdCustomerPaymentPlanGr
    {
        public cdCustomerPaymentPlanGr()
        {
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdCustomerPaymentPlanGrDescs = new HashSet<cdCustomerPaymentPlanGrDesc>();
            prCustomerPaymentPlanGrAtts = new HashSet<prCustomerPaymentPlanGrAtt>();
            prSubCurrAccs = new HashSet<prSubCurrAcc>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerPaymentPlanGrCode { get; set; }

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

        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdCustomerPaymentPlanGrDesc> cdCustomerPaymentPlanGrDescs { get; set; }
        public virtual ICollection<prCustomerPaymentPlanGrAtt> prCustomerPaymentPlanGrAtts { get; set; }
        public virtual ICollection<prSubCurrAcc> prSubCurrAccs { get; set; }
    }
}
