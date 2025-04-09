using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auDunadPFRTransactionInfo")]
    public partial class auDunadPFRTransactionInfo
    {
        public auDunadPFRTransactionInfo()
        {
        }

        [Key]
        [Required]
        public Guid DunadPFRTransactionInfoID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string V3TransactionID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DocumentNumber { get; set; }

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
