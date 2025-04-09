using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdJournalLedger")]
    public partial class cdJournalLedger
    {
        public cdJournalLedger()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string JournalLedgerCode { get; set; }

        [Required]
        public short JournalYear { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public long StartingNumber { get; set; }

        [Required]
        public long EndingNumber { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [Required]
        public decimal TurnoverDebit { get; set; }

        [Required]
        public decimal TurnoverCredit { get; set; }

        [Required]
        public double TurnoverDebitQty { get; set; }

        [Required]
        public double TurnoverCreditQty { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedUserName { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

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

    }
}
