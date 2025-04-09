using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCustomerMarkupGr")]
    public partial class cdCustomerMarkupGr
    {
        public cdCustomerMarkupGr()
        {
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdCustomerMarkupGrDescs = new HashSet<cdCustomerMarkupGrDesc>();
            prCustomerMarkupGrAtts = new HashSet<prCustomerMarkupGrAtt>();
            prSubCurrAccs = new HashSet<prSubCurrAcc>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerMarkupGrCode { get; set; }

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
        public virtual ICollection<cdCustomerMarkupGrDesc> cdCustomerMarkupGrDescs { get; set; }
        public virtual ICollection<prCustomerMarkupGrAtt> prCustomerMarkupGrAtts { get; set; }
        public virtual ICollection<prSubCurrAcc> prSubCurrAccs { get; set; }
    }
}
