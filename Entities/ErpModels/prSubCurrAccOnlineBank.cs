using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prSubCurrAccOnlineBank")]
    public partial class prSubCurrAccOnlineBank
    {
        public prSubCurrAccOnlineBank()
        {
        }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OnlineBankWebServiceCode { get; set; }

        [Required]
        public Guid SubCurrAccID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [Required]
        public byte BankOpTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt02 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt03 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt04 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt05 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt02 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt03 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt04 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt05 { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SubCurrAccOnlineBankAccountCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FirmID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FirmCode { get; set; }

        public string FirmName { get; set; }

        [Required]
        public bool AutoCreateOnlineBankTrans { get; set; }

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
        public virtual cdBankOpType cdBankOpType { get; set; }
        public virtual cdOnlineBankWebService cdOnlineBankWebService { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

    }
}
