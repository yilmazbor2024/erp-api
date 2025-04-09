using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prExpenseInvoiceConfirmationRule")]
    public partial class prExpenseInvoiceConfirmationRule
    {
        public prExpenseInvoiceConfirmationRule()
        {
            tpExpenseInvoiceConfirmations = new HashSet<tpExpenseInvoiceConfirmation>();
        }

        [Key]
        [Required]
        public Guid ExpenseInvoiceConfirmationRuleID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

      

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public byte InvoiceFormType { get; set; }

        [Required]
        public bool ConfirmationRequiredForPostingJournal { get; set; }

        [Required]
        public bool ConfirmationByV3User { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string UserGroupCode { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string UserName { get; set; }

        [Required]
        public bool ConfirmationByDepartmentManager { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobDepartmentCode { get; set; }

        [Required]
        public bool ConfirmationByDepartmentManagerOfSpecifiedWorkplace { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SpecifiedWorkplaceCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobDepartmentCodeOfSpecifiedWorkplace { get; set; }

        [Required]
        public bool ConfirmationByStoreCoordinator { get; set; }

        [Required]
        public bool ConfirmationByDepartmentManagerOfEmployee { get; set; }

        [Required]
        public bool ConfirmationNotRequiredForEInvoices { get; set; }

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

        // Navigation Properties
        public virtual cdWorkPlace cdWorkPlace { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpExpenseInvoiceConfirmation> tpExpenseInvoiceConfirmations { get; set; }
    }
}
