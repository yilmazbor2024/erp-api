using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCustomerDiscountGr")]
    public partial class cdCustomerDiscountGr
    {
        public cdCustomerDiscountGr()
        {
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdCustomerDiscountGrDescs = new HashSet<cdCustomerDiscountGrDesc>();
            prSubCurrAccs = new HashSet<prSubCurrAcc>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerDiscountGrCode { get; set; }

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
        public virtual ICollection<cdCustomerDiscountGrDesc> cdCustomerDiscountGrDescs { get; set; }
        public virtual ICollection<prSubCurrAcc> prSubCurrAccs { get; set; }
    }
}
