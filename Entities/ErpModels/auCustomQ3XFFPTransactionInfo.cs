using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auCustomQ3XFFPTransactionInfo")]
    public partial class auCustomQ3XFFPTransactionInfo
    {
        public auCustomQ3XFFPTransactionInfo()
        {
        }

        [Key]
        [Required]
        public Guid CustomQ3XFFPTransactionInfoID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string V3TransactionID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentZNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public DateTime DocumentDateTime { get; set; }

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
