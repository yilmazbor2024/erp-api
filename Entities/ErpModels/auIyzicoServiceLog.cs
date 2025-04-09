using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auIyzicoServiceLog")]
    public partial class auIyzicoServiceLog
    {
        public auIyzicoServiceLog()
        {
        }

        [Key]
        [Required]
        public Guid LogID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentId { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentTransactionId { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string OperationType { get; set; }

        public string Content { get; set; }

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
