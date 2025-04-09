using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPostAccType")]
    public partial class bsPostAccType
    {
        public bsPostAccType()
        {
            bsPostAccTypeDescs = new HashSet<bsPostAccTypeDesc>();
            prBankAdditionalChargeTypeGLAccss = new HashSet<prBankAdditionalChargeTypeGLAccs>();
            prBankPOSGLAccss = new HashSet<prBankPOSGLAccs>();
            prChequeGLAccss = new HashSet<prChequeGLAccs>();
            prCreditCardTypeGLAccss = new HashSet<prCreditCardTypeGLAccs>();
            prCurrAccGLAccounts = new HashSet<prCurrAccGLAccount>();
            prDiscountTypeGLAccss = new HashSet<prDiscountTypeGLAccs>();
            prDOVGLAccss = new HashSet<prDOVGLAccs>();
            prImportFileGLAccss = new HashSet<prImportFileGLAccs>();
            prItemAccountGrGLAccss = new HashSet<prItemAccountGrGLAccs>();
            prNotesGLAccss = new HashSet<prNotesGLAccs>();
            prOfficeGLAccss = new HashSet<prOfficeGLAccs>();
            prPaymentProviderGLAccss = new HashSet<prPaymentProviderGLAccs>();
            prPCTGLAccss = new HashSet<prPCTGLAccs>();
            prStoreBankPOSGLAccss = new HashSet<prStoreBankPOSGLAccs>();
            prStoreCustomerGLAccounts = new HashSet<prStoreCustomerGLAccount>();
            prVatGLAccss = new HashSet<prVatGLAccs>();
        }

        [Key]
        [Required]
        public byte PostAccTypeCode { get; set; }

        [Required]
        public byte GroupNum { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPostAccTypeDesc> bsPostAccTypeDescs { get; set; }
        public virtual ICollection<prBankAdditionalChargeTypeGLAccs> prBankAdditionalChargeTypeGLAccss { get; set; }
        public virtual ICollection<prBankPOSGLAccs> prBankPOSGLAccss { get; set; }
        public virtual ICollection<prChequeGLAccs> prChequeGLAccss { get; set; }
        public virtual ICollection<prCreditCardTypeGLAccs> prCreditCardTypeGLAccss { get; set; }
        public virtual ICollection<prCurrAccGLAccount> prCurrAccGLAccounts { get; set; }
        public virtual ICollection<prDiscountTypeGLAccs> prDiscountTypeGLAccss { get; set; }
        public virtual ICollection<prDOVGLAccs> prDOVGLAccss { get; set; }
        public virtual ICollection<prImportFileGLAccs> prImportFileGLAccss { get; set; }
        public virtual ICollection<prItemAccountGrGLAccs> prItemAccountGrGLAccss { get; set; }
        public virtual ICollection<prNotesGLAccs> prNotesGLAccss { get; set; }
        public virtual ICollection<prOfficeGLAccs> prOfficeGLAccss { get; set; }
        public virtual ICollection<prPaymentProviderGLAccs> prPaymentProviderGLAccss { get; set; }
        public virtual ICollection<prPCTGLAccs> prPCTGLAccss { get; set; }
        public virtual ICollection<prStoreBankPOSGLAccs> prStoreBankPOSGLAccss { get; set; }
        public virtual ICollection<prStoreCustomerGLAccount> prStoreCustomerGLAccounts { get; set; }
        public virtual ICollection<prVatGLAccs> prVatGLAccss { get; set; }
    }
}
