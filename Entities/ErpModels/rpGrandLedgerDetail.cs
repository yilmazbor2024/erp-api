using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpGrandLedgerDetail")]
    public partial class rpGrandLedgerDetail
    {
        public rpGrandLedgerDetail()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GrandLedgerCode { get; set; }

        [Key]
        [Required]
        public long LedgerEntryNumber { get; set; }

        [Required]
        public DateTime LedgerEntryDate { get; set; }

        [Key]
        [Required]
        public Guid JournalHeaderID { get; set; }

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

    }
}
