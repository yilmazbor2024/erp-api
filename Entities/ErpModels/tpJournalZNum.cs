using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpJournalZNum")]
    public partial class tpJournalZNum
    {
        public tpJournalZNum()
        {
            tpJournalZNumDetails = new HashSet<tpJournalZNumDetail>();
        }

        [Key]
        [Required]
        public Guid zNumID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [Required]

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]

        [Required]
        public short PosterminalID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CashRegisterSerialNumber { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string zNum { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdPOSTerminal cdPOSTerminal { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpJournalZNumDetail> tpJournalZNumDetails { get; set; }
    }
}
