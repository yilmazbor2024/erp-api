using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdVendorPaymentPlanGr")]
    public partial class cdVendorPaymentPlanGr
    {
        public cdVendorPaymentPlanGr()
        {
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdVendorPaymentPlanGrDescs = new HashSet<cdVendorPaymentPlanGrDesc>();
            prSubCurrAccs = new HashSet<prSubCurrAcc>();
            prVendorPaymentPlanGrAtts = new HashSet<prVendorPaymentPlanGrAtt>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VendorPaymentPlanGrCode { get; set; }

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
        public virtual ICollection<cdVendorPaymentPlanGrDesc> cdVendorPaymentPlanGrDescs { get; set; }
        public virtual ICollection<prSubCurrAcc> prSubCurrAccs { get; set; }
        public virtual ICollection<prVendorPaymentPlanGrAtt> prVendorPaymentPlanGrAtts { get; set; }
    }
}
