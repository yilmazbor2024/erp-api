using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auTRINGT202TransactionInfo")]
    public partial class auTRINGT202TransactionInfo
    {
        public auTRINGT202TransactionInfo()
        {
        }

        [Key]
        [Required]
        public Guid TRINGT202TransactionInfoID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string V3TransactionID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public DateTime DocumentDateTime { get; set; }

        [Required]
        public decimal DocumentAmount { get; set; }

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
