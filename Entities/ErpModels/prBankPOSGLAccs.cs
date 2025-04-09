using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prBankPOSGLAccs")]
    public partial class prBankPOSGLAccs
    {
        public prBankPOSGLAccs()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }
 

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string WorkPlaceCode { get; set; }

        [Key]
        [Required]
        public byte PostAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

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

        // Navigation Properties
        public virtual cdGLType cdGLType { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual bsPostAccType bsPostAccType { get; set; }
        public virtual prBankPOSAccounts prBankPOSAccounts { get; set; }

    }
}
