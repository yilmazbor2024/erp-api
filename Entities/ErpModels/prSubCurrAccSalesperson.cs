using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prSubCurrAccSalesperson")]
    public partial class prSubCurrAccSalesperson
    {
        public prSubCurrAccSalesperson()
        {
        }

        [Key]
        [Required]
        public Guid SubCurrAccID { get; set; }

        [Key]
        [Required]
        public DateTime StartDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonCode { get; set; }

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
        public virtual cdSalesperson cdSalesperson { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

    }
}
