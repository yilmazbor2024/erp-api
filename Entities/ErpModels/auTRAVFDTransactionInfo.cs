using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auTRAVFDTransactionInfo")]
    public partial class auTRAVFDTransactionInfo
    {
        public auTRAVFDTransactionInfo()
        {
        }

        [Key]
        [Required]
        public Guid TRAVFDTransactionInfoID { get; set; }

        [Required]
        public Guid V3TransactionID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string rctvnum { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string rctvcode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string znumber { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string vfdinvoicenum { get; set; }

        [Required]
        public DateTime idate { get; set; }

        [Required]
        public TimeSpan itime { get; set; }

        [Required]
        public TimeSpan senttime { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string qrpath { get; set; }

        public string qrcode_uri { get; set; }

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
